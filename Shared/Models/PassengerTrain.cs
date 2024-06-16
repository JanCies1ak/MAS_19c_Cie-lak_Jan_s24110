using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAS_19c_Cieślak_Jan_s24110.Shared.Models
{
    public class PassengerTrainToCarriage
    {
        [Range(0, 100)]//TODO: change to max carriage amount
        public required int CarriagePosition { get; set; }
        public required int TrainId { get; set; }
        public required int CarriageId { get; set; }
        public PassengerTrain Train { get; set; } = null!;
        public PassengerCarriage Carriage { get; set; } = null!;
    }

    public class PassengerTrain : Train
    {
        public List<PassengerTrainToCarriage> Carriages { get; set; } = new();

        public PassengerTrain() : base(TrainType.Passenger) { }

        [SetsRequiredMembers]
        public PassengerTrain(CargoTrain cargoTrain) : this()
        {
            Id = cargoTrain.Id;
            Model = cargoTrain.Model;
            MaxSpeed = cargoTrain.MaxSpeed;
            ManufacturerId = cargoTrain.ManufacturerId;
            Manufacturer = cargoTrain.Manufacturer;
            Departures = cargoTrain.Departures;
        }
    }
}
