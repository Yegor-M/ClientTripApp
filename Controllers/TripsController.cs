using EFCoreIntro2.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EFCoreIntro2.Dto;
using EFCoreIntro2.Models;

namespace EFCoreIntro2.Controllers
{
    [Route("api/trips")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly IDbService _dbService;

        public TripsController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTrips()
        {
            var trips = await _dbService.GetTripsAsync();
            return Ok(trips);
        }
        
        [HttpPost("/{idTrip}/clients")]
        public async Task<IActionResult> AssignCustomerToTrip([FromRoute] int idTrip,[FromBody] PostAssignClientRequest client_trip)
        {
            int status = await _dbService.AssignCustomerToTripAsync(idTrip, client_trip);
            if (status == 200  || status == 204)
            {
                return Ok();
            }
            return StatusCode(status);
        }
    }
}
