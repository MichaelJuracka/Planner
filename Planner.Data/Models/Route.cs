using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Planner.Data.Models
{
    public class Route
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int RouteId { get; set; }
        [Required]
        public DateTime DepartureDate { get; set; }
        [Required]
        public bool RouteBack { get; set; }
        public int? RegionId { get; set; }
        public int? StateId { get; set; }
        public bool AgendaCreated { get; set; }
        [ForeignKey("Provider")]
        public int ProviderId { get; set; }
        public string LicensePlate { get; set; }
        public bool OrderCreated { get; set; }
        [ForeignKey("BusType")]
        public int? BusTypeId { get; set; }
        public bool BoardingRoute { get; set; }
        public bool IsRealRoute { get; set; }
        public virtual BusType BusType { get; set; }
        public virtual Provider Provider { get; set; }
        public virtual Region Region { get; set; }
        public virtual State State { get; set; }
        public Route()
        {
            AgendaCreated = false;
            OrderCreated = false;
        }
        public override string ToString()
        {
            string message = $"Jízda ID: {RouteId}; ";
            if (!BoardingRoute)
            {
                message += Region.ToString();
                if (RouteBack)
                    message += " Zpět";
                else
                    message += " Tam";
            }
            else
            {
                if (!RouteBack)
                    message += "Svoz";
                else
                    message += "Rozvoz";
            }
            message += $"; {DepartureDate.ToShortDateString()}";
            if (IsRealRoute)
                message += $"; {LicensePlate}";
            return string.Format(message);
        }
    }
}
