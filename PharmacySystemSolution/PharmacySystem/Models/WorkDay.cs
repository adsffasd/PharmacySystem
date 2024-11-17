using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacySystem.Models
{
    [Table("WorkDay")]
    public class WorkDay
    {
        [Key]
        public int Id { get; set; }
        [StringLength(20)]
        public string? Day { get; set; }
        public int ShiftId { get; set; }
        public Shift? Shift { get; set; }
        public ICollection<Account>? Accounts { get; set; }
    }
}
