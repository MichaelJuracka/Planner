using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Planner.Data.Models
{
    public class Station
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int StationId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public string DepartureTime { get; set; }
        [Required]
        [StringLength(2048)]
        public string DeparturePlace { get; set; }
        [StringLength(2048)]
        public string Description { get; set; }
        [StringLength(100)]
        public string Gps { get; set; }
        public bool BoardingStation { get; set; }
        [ForeignKey("Region")]
        public int RegionId { get; set; }
        public int? Order { get; set; }
        public virtual Region Region { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
