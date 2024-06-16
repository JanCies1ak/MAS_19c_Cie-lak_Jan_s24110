using MAS_19c_Cieślak_Jan_s24110.Client.Pages;
using MAS_19c_Cieślak_Jan_s24110.Server.Data;
using MAS_19c_Cieślak_Jan_s24110.Shared.DTOs;
using MAS_19c_Cieślak_Jan_s24110.Shared.Models;

namespace MAS_19c_Cieślak_Jan_s24110.Server.Services
{
    public interface ITicketService
    {
        Task<TrainTicket> AddTicketAsync(TicketDto ticket);
    }

    public class TicketService : ITicketService
    {
        private readonly TrainContext _trainContext;

        public TicketService(TrainContext trainContext)
        {
            _trainContext = trainContext;
        }

        public async Task<TrainTicket> AddTicketAsync(TicketDto ticket)
        {
            TrainTicket newTicket = (await _trainContext.Tickets.AddAsync(new TrainTicket
            {
                Seat = ticket.Seat,
                PassengerId = ticket.PassengerId,
                DepartureId = ticket.DepartureId,
                CarriageId = ticket.CarriageId,
            })).Entity;
            await _trainContext.SaveChangesAsync();
            return newTicket;
        }
    }
}
