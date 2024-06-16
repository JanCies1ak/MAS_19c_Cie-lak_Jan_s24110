
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAS_19c_Cieślak_Jan_s24110.Shared.Models
{
    public class Passenger
    {
        public required int PersonId { get; set; }
        [Range(0, 100)]
        public double? PersonalDiscount { get; set; } = null;
        public List<TrainTicket> Tickets { get; set; } = new();
        public Person Person { get; set; } = null!;
    }
}
