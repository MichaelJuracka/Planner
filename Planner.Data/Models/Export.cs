using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Planner.Data.Models
{
    public class Export
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int ExportId { get; set; }
        public byte[] Data { get; set; }
        [ForeignKey("Route")]
        public int RouteId { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public virtual Route Route { get; set; }
    }
}
