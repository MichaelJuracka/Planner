using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Planner.Data.Models
{
    public class State
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int StateId { get; set; }
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        public override string ToString()
        {
            return string.Format($"{Name}");
        }
    }
}
