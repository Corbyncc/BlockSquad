using System.ComponentModel.DataAnnotations;

namespace BlockSquad.Shared.Users;
public class User
{
    public int Id { get; set; }

    public ulong SteamId { get; set; }

    [Required]
    [StringLength(maximumLength: 16, ErrorMessage = "Codename must be less than 16 characters.")]
    public string? Codename { get; set; }

    public Appearance? Appearance { get; set; }
}

