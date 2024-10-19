using Azure;
using Azure.Core;
using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.ContainerInstance;
using Azure.ResourceManager.ContainerInstance.Models;
using Azure.ResourceManager.Resources;
using BlockSquad.Api.Gslt;
using BlockSquad.Shared;
using System.Configuration;

namespace BlockSquad.Api.ServerProviders;
public class AzureServerProvider : IServerProvider
{
    private readonly IConfiguration _configuration;
    private readonly IGsltService _gsltService;

    private readonly ArmClient _armClient;

    public AzureServerProvider(IConfiguration configuration, IGsltService gsltService)
    {
        _configuration = configuration;
        _gsltService = gsltService;

        _armClient = new ArmClient(new DefaultAzureCredential());
    }

    public async Task CreateServerAsync()
    {
        var subscriptionId = _configuration["AzureSubscriptionId"] ?? throw new ConfigurationErrorsException("AzureSubscriptionId not found in configuration");
        var resourceGroupName = _configuration["AzureResourceGroupName"] ?? throw new ConfigurationErrorsException("ResourceGroupName not found in configuration");

        var containerRegistryServer = _configuration["ContainerRegistryServer"] ?? throw new ConfigurationErrorsException("ContainerRegistryServer not found in configuration");
        var containerRegistryUsername = _configuration["ContainerRegistryUsername"] ?? throw new ConfigurationErrorsException("ContainerRegistryUsername not found in configuration");
        var containerRegistryPassword = _configuration["ContainerRegistryPassword"] ?? throw new ConfigurationErrorsException("ContainerRegistryPassword not found in configuration");
        var imageName = _configuration["ImageName"] ?? throw new ConfigurationErrorsException("ContainerImageName not found in configuration");

        var guid = Guid.NewGuid();
        var containerGroupName = $"blocksquad-session-{guid}";
        var containerName = $"blocksquad-server-{guid}";

        SubscriptionResource subscription = await _armClient.GetSubscriptionResource(new ResourceIdentifier($"/subscriptions/{subscriptionId}")).GetAsync();
        ResourceGroupResource resourceGroup = await subscription.GetResourceGroups().GetAsync(resourceGroupName);

        var container = new ContainerInstanceContainer(
            containerName,
            $"{containerRegistryServer}/{imageName}",
            new ContainerResourceRequirements(new ContainerResourceRequestsContent(4.0, 2.0)));

        var gslt = await _gsltService.GetGsltAsync(guid.ToString());
        if (gslt == null)
            return;

        container.EnvironmentVariables.Add(new ContainerEnvironmentVariable("GSLT")
        {
            Value = gslt
        });

        container.Ports.Add(new ContainerPort(27015) { Protocol = ContainerNetworkProtocol.Udp });
        container.Ports.Add(new ContainerPort(27016) { Protocol = ContainerNetworkProtocol.Udp });

        var containerGroupData = new ContainerGroupData("eastus", [container], ContainerInstanceOperatingSystemType.Linux)
        {
            RestartPolicy = ContainerGroupRestartPolicy.Never,
            ImageRegistryCredentials =
                {
                    new ContainerGroupImageRegistryCredential(containerRegistryServer)
                    {
                        Username = containerRegistryUsername,
                        Password = containerRegistryPassword
                    }
                },
            IPAddress = new ContainerGroupIPAddress([new(27015) { Protocol = ContainerGroupNetworkProtocol.Udp }, new(27016) { Protocol = ContainerGroupNetworkProtocol.Udp }], ContainerGroupIPAddressType.Public),
        };

        var containerGroupCollection = resourceGroup.GetContainerGroups();
        ArmOperation<ContainerGroupResource> operation = await containerGroupCollection.CreateOrUpdateAsync(WaitUntil.Completed, containerGroupName, containerGroupData);
        ContainerGroupResource containerGroup = operation.Value;

        Console.WriteLine($"Container Group '{containerGroup.Data.Name}' created successfully.");
    }
}
