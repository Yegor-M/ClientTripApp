using System;
using EFCoreIntro2.Dto;
using EFCoreIntro2.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreIntro2.Services
{
    public class DbService : IDbService
    {
        private readonly ihordContext _dbContext;

        public DbService(ihordContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ICollection<GetTripResponse>> GetTripsAsync()
        {
            var trips = await  _dbContext.Trips
                .Include(x => x.ClientTrips).ThenInclude(x => x.IdClientNavigation)
                .Include(x => x.CountryTrips).ThenInclude(x => x.IdCountryNavigation)
                .Select(t => new GetTripResponse
                {
                    Name = t.Name,
                    Description = t.Description,
                    DateFrom = t.DateFrom,
                    DateTo = t.DateTo,
                    MaxPeople = t.MaxPeople,
                    Countries = t.CountryTrips.Select(ct => new CountryDto
                    {
                        Name = ct.IdCountryNavigation.Name
                    }).ToList(),
                    Clients = t.ClientTrips.Select(ct => new ClientDto
                    {
                        FirstName = ct.IdClientNavigation.FirstName,
                        LastName = ct.IdClientNavigation.LastName
                    }).ToList()
                }).ToListAsync();
            return trips;
        }

        public async Task<int> DeleteClientAsync(int id_client)
        {
            int status;
            
            Client c = _dbContext.Clients
                .Include(x => x.ClientTrips).ThenInclude(x => x.IdClientNavigation)
                .SingleOrDefault(x => x.IdClient == id_client);
            if (c == null || c.ClientTrips.Count() > 0)
            {
                status = 403;
            }
            else
            {
                _dbContext.Clients.Remove(c);
                _dbContext.SaveChanges();
                
                status = 204;
            }
            return status;
        }

        public async Task<int> AssignCustomerToTripAsync(int tripId, PostAssignClientRequest client_trip)
        {
            Client client;
            
            client = ClientExists(client_trip.Pesel) ? Client(client_trip.Pesel) : CreateClient(client_trip);
            
            if (client.ClientTrips.Count == 0 && TripExists(tripId))
            {
                ClientTrip new_client_trip = new ClientTrip
                {
                    IdClient = client.IdClient,
                    IdTrip = tripId,
                    RegisteredAt = DateTime.Now,
                    PaymentDate = client_trip.PaymentDate == null ? null : DateTime.Parse(client_trip.PaymentDate)
                };
                
                _dbContext.ClientTrips.Add(new_client_trip);
                _dbContext.SaveChanges();
                
                return 204;
            }
            else
            {
                return 403;
            }
        }

        public Boolean TripExists(int tripId)
        {
            return _dbContext.Trips.Any(x => x.IdTrip == tripId);
        }

        private Client Client(String pesel)
        {
            return _dbContext.Clients.Where(x => x.Pesel == pesel).SingleOrDefault();
        }
        
        private Boolean ClientExists(String pesel)
        {
            return _dbContext.Clients.Any(c => c.Pesel == pesel);
        }

        private Client CreateClient(PostAssignClientRequest client_trip)
        {
            Client new_client = new Client
            {
                Email = client_trip.Email,
                FirstName = client_trip.FirstName,
                LastName = client_trip.LastName,
                Pesel = client_trip.Pesel,
                Telephone = client_trip.Telephone
            };
            _dbContext.Clients.Add(new_client);
            _dbContext.SaveChanges();
            return new_client;
        }
    }
}
