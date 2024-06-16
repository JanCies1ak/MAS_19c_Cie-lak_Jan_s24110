using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAS_19c_Cieślak_Jan_s24110.Shared.Models
{
    public class Person
    {
        public required int Id { get; set; }
        [MaxLength(100)]
        public required string Name { get; set; } = null!;
        [MaxLength(100)]
        public required string Surname { get; set; } = null!;
        public TrainDriver? Driver { get; set; }
        public Passenger? Passenger { get; set; }

    }
}
