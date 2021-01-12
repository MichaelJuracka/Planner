using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Planner.Data.Models
{
    public class Passenger
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int PassengerId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int BusinessCase { get; set; }
        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(30)]
        public string SecondName { get; set; }
        [NotMapped]
        public string FullName { get; set; }
        [StringLength(15)]
        public string Phone { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [StringLength(2048)]
        public string AdditionalInformation { get; set; }
        [ForeignKey("Route")]
        public int RouteId { get; set; }
        [ForeignKey("BoardingRoute")]
        public int? BoardingRouteId { get; set; }
        [ForeignKey("RealRoute")]
        public int? RealRouteId { get; set; }
        [ForeignKey("BoardingStation")]
        public int BoardingStationId { get; set; }
        [ForeignKey("ExitStation")]
        public int? ExitStationId { get; set; }
        //odbaven
        public bool IsCleared { get; set; }
        [Range(0, 100)]
        public int? SeatNumber { get; set; }
        [ForeignKey("Owner")]
        public int OwnerId { get; set; }
        public string DepartureTime { get; set; }
        public virtual Route Route { get; set; }
        public virtual Route BoardingRoute { get; set; }
        public virtual Route RealRoute { get; set; }
        public virtual Station BoardingStation { get; set; }
        public virtual Station ExitStation { get; set; }
        public virtual Owner Owner { get; set; }
        public Passenger()
        {
            FullName = FirstName + " " + SecondName;
            IsCleared = false;
        }
    }
}
