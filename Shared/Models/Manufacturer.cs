using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAS_19c_Cieślak_Jan_s24110.Shared.Models
{
    public class Manufacturer
    {
        public required int Id { get; set; }
        [MaxLength(100)]
        public required string Name { get; set; } = null!;
        [MinLength(2), MaxLength(2)]
        public required string Country { get; set; } = null!;
        [NotMapped]
        public Dictionary<string, Train> Trains { get; set; } = new();

        //For serialization
        public List<Train> TrainList
        {
            get => Trains.Values.ToList();
            set => Trains = value.ToDictionary(train => train.Model);
        }
    }
}
