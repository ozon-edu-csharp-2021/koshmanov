using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OzonEdu.Merchandise.Application.Commands.CreateMerchOrder;
using OzonEdu.Merchandise.Application.Queries.FindById;
using OzonEdu.Merchandise.Infrastructure.Repositories.Infrastructure.Interfaces;
using OzonEdu.Merchandise.Models;


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

        [HttpGet("{id:int}/{merchId:int}")]//
        public async Task<ActionResult<GetMerchResponse>> GetMerch([FromRoute]int id, [FromRoute]int merchId,
            CancellationToken token)
        {;
            CreateMerchOrderCommand createCommand = new CreateMerchOrderCommand()
            {
                EmloyeeId = id,
                MerchPackType = merchId
            };
            var result = await _mediator.Send(createCommand);
            return Ok(result);
        }
        
        [HttpGet("{id:int}")]//
        public async Task<ActionResult<GetMerchResponse>> GetMerchOrderById([FromRoute]int id,
            CancellationToken token)
        {
            FindMerchOrderByIdQuery findQuery = new FindMerchOrderByIdQuery()
            {
                Id = id
            };
            var res = await _mediator.Send(findQuery, token);
     
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