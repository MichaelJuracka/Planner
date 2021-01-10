using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Planner.Data.Models
{
    public class Provider
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int ProviderId { get; set; }
        [Required]
        [StringLength(2048)]
        public string Name { get; set; }
        public override string ToString()
        {
            return string.Format(Name);
        }
    }
}
