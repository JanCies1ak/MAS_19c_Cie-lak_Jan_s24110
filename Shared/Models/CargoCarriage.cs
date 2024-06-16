using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAS_19c_Cieślak_Jan_s24110.Shared.Models
{
    public class CargoCarriage : Carriage
    {
        public CargoTrainToCarriage Train { get; set; } = null!;

        public override void ShowDetails()
        {
            Console.WriteLine($"CargoCarriage: [Train: {Train.TrainId}]");
        }

        public void ConnectTo(CargoTrain train) => throw new NotImplementedException();
    }
}
