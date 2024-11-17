using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacySystem.Models
{
    [Table("Product")]
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string? Manufacturer { get; set; }
        public int CategoryId { get; set; }

        public Category? Category { get; set; }
        public ICollection<Bill>? Bills { get; set; }
        public ICollection<Inventory>? Inventories { get; set; }
        public ICollection<ImpRepoDetail>? impRepoDetails { get; set; }
        public ICollection<ExpRepo>? ExpRepos { get; set; }
    }
}
