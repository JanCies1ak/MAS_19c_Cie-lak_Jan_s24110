using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAS_19c_Cieślak_Jan_s24110.Shared.Models
{
    public class TrainTicket
    {
        public required int Seat { get; set; }
        public required int PassengerId { get; set; }
        public required int DepartureId { get; set; }
        public required int CarriageId { get; set; }
        public Passenger Passenger { get; set; } = null!;
        public Departure Departure { get; set; } = null!;
        public PassengerCarriage Carriage { get; set; } = null!;
    }
}
