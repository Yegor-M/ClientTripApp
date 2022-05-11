using EFCoreIntro2.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EFCoreIntro2.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IDbService _dbService;

        public ClientsController(IDbService dbService)
        {
            _dbService = dbService;
        }
        
        [HttpDelete("{idClient}")]
        public async Task<IActionResult> DeleteClient([FromRoute] int idClient)
        {
            int status = await _dbService.DeleteClientAsync(idClient);
            
            if (status == 200  || status == 204)
            {
                return Ok();
            }
            return StatusCode(status);
        }
    }
}