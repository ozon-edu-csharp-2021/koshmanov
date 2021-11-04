using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OzonEdu.Merchandise.Infrastructure.Commands.CreateMerchOrder;
using OzonEdu.Merchandise.Infrastructure.Commands.FindById;
using OzonEdu.Merchandise.Infrastructure.Handlers;
using OzonEdu.Merchandise.Models;
using OzonEdu.Merchandise.Services.Interfaces;

namespace OzonEdu.Merchandise.Controllers 
{
    [ApiController]
    [Route("v1/api/merch")]
    public class MerchandiseController : ControllerBase
    {
        private readonly IMerchandiseService _merchService;
        private readonly IMediator _mediator;

        public MerchandiseController(IMerchandiseService merchService, IMediator mediator)
        {
            _merchService = merchService;
            _mediator = mediator;
        }

        [HttpGet("{id:long}/{itemName}")]//
        public async Task<ActionResult<GetMerchResponse>> GetMerch([FromRoute]long id, [FromRoute]string itemName,
            CancellationToken token)
        {;
            var request = new GetMerchRequest(new Employee(id, "Bob"), new MerchItem(itemName));
            var merch = await _merchService.GetMerch( request,token);
            if (merch is null)
                return NotFound();
            return Ok(merch);
        }
        
        [HttpGet("{id:long}")]//
        public async Task<ActionResult<GetMerchResponse>> GetMerchOrderById([FromRoute]long id,
            CancellationToken token)
        {
            FindMerchOrderByIdCommand findCommand = new FindMerchOrderByIdCommand()
            {
                Id = id
            };
            var res = await _mediator.Send(findCommand, token);
     
            return Ok(res);
        }
        
        [HttpGet]
        public async Task<ActionResult<GetOrderStateResponse>> GetMerchOrderState([FromQuery] long id ,
            CancellationToken token)
        { 
            var request = new GetOrderStateRequest( new MerchOrder(id, new List<MerchItem>()));

            var merch = await _merchService.GetMerchOrderState(request, token);
            return merch;
        }
        
        
    }
}