using MAS_19c_Cieślak_Jan_s24110.Server.Data;
using MAS_19c_Cieślak_Jan_s24110.Shared.DTOs;
using MAS_19c_Cieślak_Jan_s24110.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Runtime.Intrinsics.Arm;

namespace MAS_19c_Cieślak_Jan_s24110.Server.Services
{
    public interface IDepartureService
    {
        Task<List<Departure>> GetFullDeparturesAsync();
        Task<Departure> GetDepartureAsync(int departureId);
    }

    public class DepartureService : IDepartureService
    {
        private readonly TrainContext _trainContext;

        public DepartureService(TrainContext trainContext)
        {
            _trainContext = trainContext;
        }

        public async Task<Departure> GetDepartureAsync(int departureId)
        {
            return await _trainContext.Departures.Where(dep => dep.Id == departureId).FirstAsync();
        }

        public async Task<List<Departure>> GetFullDeparturesAsync()
        {
            var departures = await _trainContext.Departures
                .Include(dep => dep.Train)
                .Include(dep => dep.TrainDriver)
                .Include(dep => dep.Route)
                    .ThenInclude(route => route.Stops)
                    .ThenInclude(routeStop => routeStop.Stop)
                .Include(dep => dep.Tickets)
                    .ThenInclude(ticket => ticket.Carriage)
                .ToListAsync();

            var trains = await _trainContext.PassengerTrains
                .Include(train => train.Carriages)
                    .ThenInclude(trainToCar => trainToCar.Carriage)
                .ToListAsync();

            foreach(var dep in departures)
            {
                var train = trains.FirstOrDefault(t => t.Id == dep.TrainId);
                if (train != null)
                {
                    dep.Train = train;
                }
            }

            return departures;
        }
    }
}
