using MAS_19c_Cieślak_Jan_s24110.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAS_19c_Cieślak_Jan_s24110.Shared.DTOs
{
    public class DepartureWithSeatsDto
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int TicketPrice { get; set; }
        public int TrainId { get; set; }
        public int RouteId { get; set; }
        public int TrainDriverId { get; set; }
        public List<string> StopsNames { get; set; } = new();
        public string TrainModel { get; set; } = null!;
        public List<SeatDto>? Seats { get; set; } = new();
    }
}
