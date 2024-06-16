using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAS_19c_Cieślak_Jan_s24110.Shared.Models
{
    public enum PassengerCarriageType
    {
        Passenger,
        Restaurant,
    }

    public class PassengerCarriage : Carriage
    {
        public required PassengerCarriageType Type { get; set; }
        [Range(0, 100)]//TODO: Change to max carriage amount
        public required int SeatAmount { get; set; }
        public PassengerTrainToCarriage Train { get; set; } = null!;
        public List<TrainTicket> Tickets { get; set; } = null!;

        public override void ShowDetails()
        {
            Console.WriteLine($"PassengerCarriage: [SeatAmount: {SeatAmount}, Train: {Train.TrainId}, Type: {Type}]");
        }

        public void ConnectTo(PassengerTrain train) => throw new NotImplementedException();
    }
}
