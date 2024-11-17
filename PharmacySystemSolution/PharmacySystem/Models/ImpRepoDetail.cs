using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacySystem.Models
{
    [Table("ImpRepoDetail")]
    public class ImpRepoDetail
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int ImpRepoId { get; set; }
        public ImpRepo? ImpRepo { get; set; }
    }
}
