using System.Threading.Tasks;

namespace BlockSquad.Shared;
public interface IServerProvider
{
    Task CreateServerAsync();
}