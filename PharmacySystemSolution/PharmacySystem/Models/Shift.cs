using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacySystem.Models
{
    [Table("Shift")]
    public class Shift
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        [StringLength(10)]
        public string? StartTime { get; set; }
        [StringLength(10)]
        public string? EndTime { get; set; }
        public ICollection<WorkDay>? WorkDays { get; set; }
    }
}
