using MAS_19c_Cieślak_Jan_s24110.Shared.Models;
using MAS_19c_Cieślak_Jan_s24110.Shared.Utils;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace MAS_19c_Cieślak_Jan_s24110.Server.Data
{
    public class TrainContext : DbContext
    {
        public TrainContext(DbContextOptions options) : base(options)
        {
        }

        public TrainContext() : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);//TODO: Change this
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TrainDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Train> Trains { get; set; }
        public DbSet<PassengerTrain> PassengerTrains { get; set; }
        public DbSet<PassengerTrainToCarriage> PassengerTrainToCarriages { get; set; }
        public DbSet<CargoTrain> CargoTrains { get; set; }
        public DbSet<CargoTrainToCarriage> CargoTrainToCarriages { get; set; }
        public DbSet<Departure> Departures { get; set; }
        public DbSet<TrainTicket> Tickets { get; set; }
        public DbSet<TrainRoute> Routes { get; set; }
        public DbSet<RouteStop> RouteStops { get; set; }
        public DbSet<TrainStop> Stops { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<TrainDriver> Drivers { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Carriage> Carriages { get; set; }
        public DbSet<CargoCarriage> CargoCarriages { get; set; }
        public DbSet<PassengerCarriage> PassengerCarriages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Manufacturer>(entity => {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Train>(entity => {
                entity.UseTphMappingStrategy();

                entity.HasDiscriminator(e => e.Type)
                    .HasValue<PassengerTrain>(TrainType.Passenger)
                    .HasValue<CargoTrain>(TrainType.Cargo)
                    .HasValue<Train>(TrainType.Error);

                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Manufacturer)
                    .WithMany(e => e.TrainList)
                    .HasForeignKey(e => e.ManufacturerId);
            });

            modelBuilder.Entity<PassengerTrainToCarriage>(entity => {
                entity.HasKey(e => new
                {
                    Train = e.TrainId,
                    Carriage = e.CarriageId,
                });
                entity.HasOne(e => e.Train)
                    .WithMany(e => e.Carriages)
                    .HasForeignKey(e => e.TrainId);

                entity.HasOne(e => e.Carriage)
                    .WithOne(e => e.Train)
                    .HasForeignKey<PassengerTrainToCarriage>(e => e.CarriageId);

            });

            modelBuilder.Entity<CargoTrainToCarriage>(entity => {
                entity.HasKey(e => new
                {
                    Train = e.TrainId,
                    Carriage = e.CarriageId,
                });
                entity.HasOne(e => e.Train)
                    .WithMany(e => e.Carriages)
                    .HasForeignKey(e => e.TrainId);

                entity.HasOne(e => e.Carriage)
                    .WithOne(e => e.Train)
                    .HasForeignKey<CargoTrainToCarriage>(e => e.CarriageId);
            });

            modelBuilder.Entity<Departure>(entity => {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Train)
                    .WithMany(e => e.Departures)
                    .HasForeignKey(e => e.TrainId);

                entity.HasOne(e => e.Route)
                    .WithMany(e => e.Departures)
                    .HasForeignKey(e => e.RouteId);

                entity.HasOne(e => e.TrainDriver)
                    .WithMany(e => e.Departures)
                    .HasForeignKey(e => e.TrainDriverId);
            });

            modelBuilder.Entity<TrainTicket>(entity => {
                entity.HasKey(e => new
                {
                    Passenger = e.PassengerId,
                    Departure = e.DepartureId,
                    Carriage = e.CarriageId,
                    Seat = e.Seat,
                });

                entity.HasOne(e => e.Passenger)
                    .WithMany(e => e.Tickets)
                    .HasForeignKey(e => e.PassengerId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(e => e.Departure)
                    .WithMany(e => e.Tickets)
                    .HasForeignKey(e => e.DepartureId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(e => e.Carriage)
                    .WithMany(e => e.Tickets)
                    .HasForeignKey(e => e.CarriageId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<TrainRoute>(entity => {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<RouteStop>(entity => {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Route)
                    .WithMany(e => e.Stops)
                    .HasForeignKey(e => e.RouteId);

                entity.HasOne(e => e.Stop)
                    .WithMany(e => e.Routes)
                    .HasForeignKey(e => e.StopId);
            });

            modelBuilder.Entity<TrainStop>(entity => {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Location)
                .HasConversion(
                    desirealized => JsonConvert.SerializeObject(desirealized),
                    serialized => serialized == null ? null : JsonConvert.DeserializeObject<Location>(serialized)
                );
            });

            modelBuilder.Entity<Person>(entity => {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<TrainDriver>(entity => {
                entity.HasKey(e => e.PersonId);

                entity.HasOne(e => e.Person)
                    .WithOne(e => e.Driver)
                    .HasForeignKey<TrainDriver>(e => e.PersonId);
            });

            modelBuilder.Entity<Passenger>(entity => {
                entity.HasKey(e => e.PersonId);

                entity.HasOne(e => e.Person)
                    .WithOne(e => e.Passenger)
                    .HasForeignKey<Passenger>(e => e.PersonId);
            });

            modelBuilder.Entity<Carriage>().UseTpcMappingStrategy();
            modelBuilder.Entity<Carriage>(entity => {
                entity.HasKey(e => e.Id);
            });

            Seed(modelBuilder);
        }

        private void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>(entity => {
                entity.HasData(new List<Person>
                {
                    new()
                    {
                        Id = 1,
                        Name = "John",
                        Surname = "Doe",
                    },
                    new()
                    {
                        Id = 2,
                        Name = "Dri",
                        Surname = "Ver",
                    }
                });
            });

            modelBuilder.Entity<Passenger>(entity => {
                entity.HasData(new List<Passenger>
                {
                    new()
                    {
                        PersonId = 1,
                    }
                });
            });

            modelBuilder.Entity<TrainDriver>(entity => {
                entity.HasData(new List<TrainDriver>
                {
                    new()
                    {
                        PersonId = 2,
                        Salary = 10000_00,
                        Qualification = DriverQualification.Expert,
                        PhoneNumber = "123123123",
                    }
                });
            });

            modelBuilder.Entity<Manufacturer>(entity => {
                entity.HasData(new List<Manufacturer>
                {
                    new()
                    {
                        Id = 1,
                        Name = "Strong trains",
                        Country = "PL",
                    }
                });
            });

            modelBuilder.Entity<PassengerTrain>(entity => {
                entity.HasData(new List<PassengerTrain>
                {
                    new()
                    {
                        Id = 1,
                        Model = "MoDel-1",
                        MaxSpeed = 300,
                        ManufacturerId = 1,
                    }
                });
            });

            modelBuilder.Entity<TrainRoute>(entity => {
                entity.HasData(new List<TrainRoute>
                {
                    new()
                    {
                        Id = 1,
                        Length = 700,
                    },
                    new()
                    {
                        Id = 2,
                        Length = 800,
                    }
                });
            });

            modelBuilder.Entity<Departure>(entity => {
                entity.HasData(new List<Departure>
                {
                    new()
                    {
                        Id = 1,
                        StartTime = DateTime.Now.AddDays(1),
                        EndTime = DateTime.Now.AddDays(1).AddHours(7),
                        TicketPrice = 100_00,
                        TrainId = 1,
                        RouteId = 1,
                        TrainDriverId = 2,
                    },
                    new()
                    {
                        Id = 2,
                        StartTime = DateTime.Now.AddDays(2),
                        EndTime = DateTime.Now.AddDays(2).AddHours(7),
                        TicketPrice = 60_60,
                        TrainId = 1,
                        RouteId = 2,
                        TrainDriverId = 2,
                    }
                });
            });

            modelBuilder.Entity<TrainStop>(entity => {
                entity.HasData(new List<TrainStop>
                {
                    new()
                    {
                        Id = 1,
                        Name = "Warsaw",
                        Location = new()
                        {
                            Latitude = "52.246457",
                            Longitude = "21.076526",
                        }
                    },
                    new()
                    {
                        Id = 2,
                        Name = "Lublin",
                        Location = new()
                        {
                            Latitude = "51.235103",
                            Longitude = "22.607200",
                        }
                    },
                    new()
                    {
                        Id = 3,
                        Name = "Krakow",
                        Location = new()
                        {
                            Latitude = "50.034877",
                            Longitude = "19.941608",
                        }
                    }
                });
            });

            modelBuilder.Entity<RouteStop>(entity => {
                entity.HasData(new List<RouteStop>
                {
                    new()
                    {
                        Id = 1,
                        RouteId = 1,
                        StopId = 1,
                    },
                    new()
                    {
                        Id = 2,
                        RouteId = 1,
                        StopId = 2,
                    },
                    new()
                    {
                        Id = 3,
                        RouteId = 2,
                        StopId = 1,
                    },
                    new()
                    {
                        Id = 4,
                        RouteId = 2,
                        StopId = 3,
                    }
                });
            });

            modelBuilder.Entity<PassengerCarriage>(entity => {
                entity.HasData(new List<PassengerCarriage>()
                {
                    new()
                    {
                        Id = 1,
                        SeatAmount = 6,
                        Type = PassengerCarriageType.Passenger,
                    },
                    new()
                    {
                        Id = 2,
                        SeatAmount = 6,
                        Type = PassengerCarriageType.Passenger,
                    }
                });
            });

            modelBuilder.Entity<PassengerTrainToCarriage>(entity => {
                entity.HasData(new List<PassengerTrainToCarriage>()
                {
                    new()
                    {
                        CarriageId = 1,
                        TrainId = 1,
                        CarriagePosition = 1,
                    },
                    new()
                    {
                        CarriageId = 2,
                        TrainId = 1,
                        CarriagePosition = 2,
                    },
                });
            });

            modelBuilder.Entity<TrainTicket>(entity => {
                entity.HasData(new List<TrainTicket>
                {
                    new()
                    {
                        Seat = 1,
                        PassengerId = 1,
                        DepartureId = 1,
                        CarriageId = 1
                    }
                });
            });
        }
    }
}
