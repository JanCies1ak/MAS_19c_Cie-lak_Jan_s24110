using MAS_19c_Cieślak_Jan_s24110.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAS_19c_Cieślak_Jan_s24110.Shared.DTOs
{
    public class TicketDto
    {
        public int Seat { get; set; }
        public int PassengerId { get; set; }
        public int CarriageId { get; set; }
        public int DepartureId { get; set; }
    }
}
