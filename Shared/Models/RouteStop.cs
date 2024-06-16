using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAS_19c_Cieślak_Jan_s24110.Shared.Models
{
    public class RouteStop
    {
        public required int Id { get; set; }
        public required int RouteId { get; set; }
        public required int StopId { get; set; }
        public TrainRoute Route { get; set; } = null!;
        public TrainStop Stop { get; set; } = null!;
    }
}
