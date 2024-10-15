using System.ComponentModel.DataAnnotations;

namespace BlockSquad.Shared.Users
{
    public class Appearance
    {

        public int Id { get; set; }

        [Required]
        public byte FaceId { get; set; }

        [Required]
        public byte HairId { get; set; }

        [Required]
        public byte BeardId { get; set; }

        [Required]
        [RegularExpression(@"^#([A-Fa-f0-9]{6})$")]
        public string? SkinColor { get; set; }

        [Required]
        [RegularExpression(@"^#([A-Fa-f0-9]{6})$")]
        public string? HairColor { get; set; }

        public int UserId { get; set; } // One to One relationship with User entity
    }
}
