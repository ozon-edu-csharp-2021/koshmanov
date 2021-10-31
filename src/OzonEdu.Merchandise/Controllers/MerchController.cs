using System.Collections.Generic;
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

        [HttpGet("{id:long}/{itemName}")]//
        public async Task<ActionResult<GetMerchResponse>> GetMerch([FromRoute]long id, [FromRoute]string itemName,
            CancellationToken token)
        {
            var request = new GetMerchRequest(new Employee(id, "Bob"), new MerchItem(itemName));
            var merch = await _merchService.GetMerch( request,token);
            if (merch is null)
                return NotFound();
            return Ok(merch);
        }
        
        [HttpGet]
        public async Task<ActionResult<GetOrderStateResponse>> GetMerchGetMerchOrderState([FromQuery] long id ,
            CancellationToken token)
        { 
            var request = new GetOrderStateRequest( new MerchOrder(id, new List<MerchItem>()));

            var merch = await _merchService.GetMerchOrderState(request, token);
            return merch;
        }
    }
}