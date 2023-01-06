

using ItAssetManagement.Enums;
using System.ComponentModel.DataAnnotations;

namespace ItAssetManagement.Models
{
    public class Asset
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? PurchasedDate { get; set; }

        public string? SerialNo { get; set; }

        public AssetType AssetType { get; set; }

        public DurationType DurationType { get; set; }

        public string? EnteredBy { get; set; }

    }

}
