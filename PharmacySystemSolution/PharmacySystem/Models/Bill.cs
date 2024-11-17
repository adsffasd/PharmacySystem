using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacySystem.Models
{
    [Table("Bill")]
    public class Bill
    {
        [Key]
        public int Id { get; set; }
        public int Amount { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public int AccountId { get; set; }
        public Account? Account { get; set; }
    }
}
