using BlockSquad.Shared.Users;
using Microsoft.AspNetCore.Mvc;

namespace BlockSquad.API.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser([FromQuery] int? userId = null, [FromQuery] ulong? steamId = null)
        {
            User? user = null;

            if (userId.HasValue)
                user = await _usersService.GetUserAsync(userId.Value);

            if (steamId.HasValue)
                user = await _usersService.GetUserBySteamIdAsync(steamId.Value);

            if (user == null)
                return NotFound(new { message = $"Failed to find user by given Id" });

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User newUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdUser = await _usersService.CreateUserAsync(newUser);

            return CreatedAtAction(nameof(CreateUser), createdUser);
        }
    }
}
