using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Planner.Data.Models
{
    public class Owner
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int OwnerId { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
