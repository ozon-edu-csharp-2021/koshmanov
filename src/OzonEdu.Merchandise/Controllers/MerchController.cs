using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OzonEdu.Merchandise.Models;
using OzonEdu.Merchandise.Services.Interfaces;

namespace OzonEdu.Merchandise.Controllers 
{
    [ApiController]
    [Route("v1/api/merch")]
    public class MerchandiseController : ControllerBase
    {
        private readonly IMerchandiseService _merchService;

        public MerchandiseController(IMerchandiseService merchService)
        {
            _merchService = merchService;
        }

        [HttpGet]
        public async Task<ActionResult<GetMerchResponseModel>> GetMerch(
            CancellationToken token)
        {
            var merch = await _merchService.GetMerch( token);
            if (merch is null)
                return NotFound();
            return Ok(merch);
        }
        
        [HttpGet("{id:long}")]
        public async Task<ActionResult<GetMerchOrderStateResponseModel>> GetMerchGetMerchOrderState([FromQuery] long id ,
            CancellationToken token)
        {
            var merch = await _merchService.GetMerchOrderState(id, token);
            return merch;
        }
    }
}