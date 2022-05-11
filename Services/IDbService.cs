using EFCoreIntro2.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreIntro2.Models;

namespace EFCoreIntro2.Services
{
    public interface IDbService
    {
        Task<ICollection<GetTripResponse>> GetTripsAsync();
        Task<int> DeleteClientAsync(int idTrip);
        Task<int> AssignCustomerToTripAsync(int idTrip, PostAssignClientRequest client);
    }
}
