using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacySystem.Models
{
    [Table("ImpRepo")]
    public class ImpRepo
    {
        [Key]
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalAmount { get; set; }
        public bool Status { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public int SupplierId { get; set; }
        public Supplier? Supplier { get; set; }
        public ICollection<ImpRepoDetail>? impRepoDetails { get; set; }
    }
}
