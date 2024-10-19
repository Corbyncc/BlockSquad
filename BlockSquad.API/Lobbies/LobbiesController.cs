using BlockSquad.Shared;
using BlockSquad.Shared.Lobbies;
using Microsoft.AspNetCore.Mvc;

namespace BlockSquad.Api.Lobbies;
[Route("api/[controller]")]
[ApiController]
public class LobbiesController : ControllerBase
{
    private readonly ILobbiesService _lobbiesService;
    private readonly IServerProvider _serverProvider;

    public LobbiesController(ILobbiesService lobbiesService, IServerProvider serverProvider)
    {
        _lobbiesService = lobbiesService;
        _serverProvider = serverProvider;
    }

    [HttpGet]
    public async Task<IActionResult> GetLobbies()
    {
        return Ok(await _lobbiesService.GetLobbies());
    }

    [HttpGet("{lobbyId}")]
    public async Task<IActionResult> GetLobby(int lobbyId)
    {
        var lobby = await _lobbiesService.GetLobbyAsync(lobbyId);
        if (lobby == null)
            return NotFound("Lobby not found");

        return Ok(lobby);
    }

    [HttpPost]
    public async Task<IActionResult> CreateLobby([FromBody] Lobby newLobby)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var createdLobby = await _lobbiesService.CreateLobbyAsync(newLobby);
        if (createdLobby == null)
            return Problem("Unknown error creating lobby");

        await _serverProvider.CreateServerAsync();

        // Assuming `createdLobby` has a property `Id` that represents the lobby ID.
        return CreatedAtAction(nameof(GetLobby), new { lobbyId = createdLobby.Id }, createdLobby);
    }

    [HttpPut("{lobbyId}")]
    public async Task<IActionResult> UpdateLobby(int lobbyId, [FromBody] Lobby updatedLobby)
    {
        var lobby = await _lobbiesService.UpdateLobbyAsync(lobbyId, updatedLobby);
        if (lobby == null)
            return NotFound("Lobby not found");

        return NoContent();
    }
}
