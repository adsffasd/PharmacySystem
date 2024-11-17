using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacySystem.Models
{
    [Table("Inventory")]
    public class Inventory
    {
        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; }
        public DateTime LastUpdate { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
