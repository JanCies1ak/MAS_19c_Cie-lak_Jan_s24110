using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAS_19c_Cieślak_Jan_s24110.Shared.Models
{
    public class CargoTrainToCarriage
    {
        public required int CarriagePosition { get; set; }
        public required int TrainId { get; set; }
        public required int CarriageId { get; set; }
        public CargoTrain Train { get; set; } = null!;
        public CargoCarriage Carriage { get; set; } = null!;
    }

    public class CargoTrain : Train
    {
        public List<CargoTrainToCarriage> Carriages { get; set; } = new();

        public CargoTrain() : base(TrainType.Cargo) { }

        [SetsRequiredMembers]
        public CargoTrain(PassengerTrain passengerTrain) : this()
        {
            Id = passengerTrain.Id;
            Model = passengerTrain.Model;
            MaxSpeed = passengerTrain.MaxSpeed;
            ManufacturerId = passengerTrain.ManufacturerId;
            Manufacturer = passengerTrain.Manufacturer;
            Departures = passengerTrain.Departures;
        }
    }
}
