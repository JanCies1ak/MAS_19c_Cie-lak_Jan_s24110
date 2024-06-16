using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAS_19c_Cieślak_Jan_s24110.Shared.Models
{
    public enum DriverQualification
    {
        Beginner,
        Intermediate,
        Expert,
    }

    public class TrainDriver
    {
        public required int PersonId { get; set; }
        [Range(0, int.MaxValue)]
        public required int Salary { get; set; }
        public required DriverQualification Qualification { get; set; }
        [NotMapped]
        public int MaxCarriageAmount => Qualification switch
        {
            DriverQualification.Beginner => 5,
            DriverQualification.Intermediate => 12,
            DriverQualification.Expert => int.MaxValue,
            _ => throw new NotSupportedException("Provided driver qualification is not supported."),
        };
        [Phone]
        public required string PhoneNumber { get; set; } = null!;
        public Person Person { get; set; } = null!;
        public List<Departure> Departures { get; set; } = new();
    }
}
