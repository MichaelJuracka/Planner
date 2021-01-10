using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Planner.Data.Models
{
    public class BusType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int BusTypeId { get; set; }
        [Range(0, 100)]
        [Required]
        public int NumberOfSeats { get; set; }
        [StringLength(200)]
        [Required]
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
