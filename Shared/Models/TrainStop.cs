using MAS_19c_Cieślak_Jan_s24110.Shared.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAS_19c_Cieślak_Jan_s24110.Shared.Models
{

    public class TrainStop
    {
        public required int Id { get; set; }
        [MaxLength(100)]
        public required string Name { get; set; } = null!;
        public required Location Location { get; set; } = null!;
        public List<RouteStop> Routes { get; set; } = null!;
    }
}
