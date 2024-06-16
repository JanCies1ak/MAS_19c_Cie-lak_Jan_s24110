using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAS_19c_Cieślak_Jan_s24110.Shared.Models
{
    public class TrainRoute
    {
        public required int Id { get; set; }
        [Range(0, double.MaxValue)]
        public required double Length { get; set; }
        public List<Departure> Departures { get; set; } = new();
        public List<RouteStop> Stops { get; set; } = new();
    }
}
