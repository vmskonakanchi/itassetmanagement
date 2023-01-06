


using System.ComponentModel.DataAnnotations;

namespace ItAssetManagement.Models
{
    public class AssetRequest
    {
        [Key]
        public uint Id { get; set; }

        public string? FromDate { get; set; }

        public string? ToDate { get; set; }

        public string? Type { get; set; }

        [Required]
        public string? AssetName { get; set; }

        [Required]
        public int AssetId { get; set; }

        [Required]
        public string? UserName { get; set; }

        public string? Status { get; set; } = "Pending";

    }
}