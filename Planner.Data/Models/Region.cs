using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Planner.Data.Models
{
    public class Region
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int RegionId { get; set; }
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        public int StateId { get; set; }
        public virtual State State { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
