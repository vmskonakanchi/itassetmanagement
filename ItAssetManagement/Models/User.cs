using System.ComponentModel.DataAnnotations;

namespace ItAssetManagement.Models
{
    public class User
    {
        [Key]
        public uint Id { get; set; }

        public string? Name { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }


        public ulong Phone { get; set; }
    }
}
