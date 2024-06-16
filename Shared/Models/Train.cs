using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAS_19c_Cieślak_Jan_s24110.Shared.Models
{
    public enum TrainType
    {
        Passenger = 0,
        Cargo = 1,
        Error = 2,
    }

    public class Train
    {
        //TODO: max carriage amount in properties
        public required int Id { get; set; }
        public TrainType Type { get; private set; }
        [MaxLength(20)]
        public required string Model { get; set; } = null!;
        [Range(0, 2000)]
        public required double MaxSpeed { get; set; }
        public required int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; } = null!;
        public List<Departure> Departures { get; set; } = new();

        public Train(TrainType type)
        {
            Type = type;
        }
        //TODO: add all methods
    }
}
