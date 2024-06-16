using MAS_19c_Cieślak_Jan_s24110.Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAS_19c_Cieślak_Jan_s24110.Shared.Models
{
    public class Departure
    {
        private DateTime _endTime;

        public required int Id { get; set; }
        public required DateTime StartTime { get; set; }
        public required DateTime EndTime
        {
            get => _endTime;
            set
            {
                if (value < StartTime)
                    throw new ArgumentOutOfRangeException(nameof(EndTime));
                _endTime = value;
            }
        }

        [Range(0, int.MaxValue)]
        public required int TicketPrice { get; set; }
        public required int TrainId { get; set; }
        public required int RouteId { get; set; }
        public required int TrainDriverId { get; set; }
        public Train Train { get; set; } = null!;
        public TrainRoute Route { get; set; } = null!;
        public TrainDriver TrainDriver { get; set; } = null!;
        public List<TrainTicket> Tickets { get; set; } = new();

        public List<SeatDto>? GetSeats()
        {
            if (Train is not PassengerTrain)
                return null;

            var passengerTrain = (PassengerTrain) Train;

            List<SeatDto> seats = new();
            foreach (var trainToCarriage in passengerTrain.Carriages)
            {
                for (var i = 0; i < trainToCarriage.Carriage.SeatAmount; i++)
                {
                    var seat = new SeatDto
                    {
                        CarriagePosition = trainToCarriage.CarriagePosition,
                        Number = i + 1,
                        Available = !Tickets.Any(ticket => ticket.CarriageId == trainToCarriage.CarriageId
                            && ticket.DepartureId == Id
                            && ticket.Seat == i + 1),
                    };
                    seats.Add(seat);
                }
            }
            return seats;
        }
    }
}
