using MAS_19c_Cieślak_Jan_s24110.Server.Services;
using MAS_19c_Cieślak_Jan_s24110.Shared.DTOs;
using MAS_19c_Cieślak_Jan_s24110.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace MAS_19c_Cieślak_Jan_s24110.Server.Controllers
{
    [ApiController]
    [Route("api/departures")]
    public class DepartureController : ControllerBase
    {
        private readonly ILogger<DepartureController> _logger;
        private readonly IDepartureService _departureService;
        private readonly ITicketService _ticketService;

        public DepartureController(ILogger<DepartureController> logger, IDepartureService departureService, ITicketService ticketService)
        {
            _logger = logger;
            _departureService = departureService;
            _ticketService = ticketService;
        }

        [HttpGet]
        [Route("{departureId}")]
        public async Task<IActionResult> GetDepartures(int departureId)
        {
            return Ok(await _departureService.GetDepartureAsync(departureId));
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartures()
        {
            var departureList = (await _departureService.GetFullDeparturesAsync())?
                .Select(dep => new DepartureWithSeatsDto
                    {
                        Id = dep.Id,
                        StartTime = dep.StartTime,
                        EndTime = dep.EndTime,
                        TicketPrice = dep.TicketPrice,
                        TrainId = dep.TrainId,
                        RouteId = dep.RouteId,
                        TrainDriverId = dep.TrainDriverId,
                        StopsNames = dep.Route.Stops.Select(routeStop => routeStop.Stop.Name).ToList(),
                        TrainModel = dep.Train.Model,
                        Seats = dep.GetSeats(),
                    })
                .ToList();
            return base.Ok(departureList);
        }

        [HttpPut]
        [Route("{departureId}/tickets/buy")]
        public async Task<IActionResult> BuyTicketForDeparture(int departureId, TicketDto ticket)
        {
            return Ok(await _ticketService.AddTicketAsync(ticket));
        }
    }
}
