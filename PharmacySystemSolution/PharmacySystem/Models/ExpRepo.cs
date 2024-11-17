using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacySystem.Models
{
    [Table("ExpRepo")]
    public class ExpRepo
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool Status { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
