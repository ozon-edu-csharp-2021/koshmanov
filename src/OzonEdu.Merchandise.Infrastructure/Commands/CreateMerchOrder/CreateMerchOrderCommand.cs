using System.Collections.Generic;
using MediatR;
using OzonEdu.Merchandise.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate;

namespace OzonEdu.Merchandise.Infrastructure.Commands.CreateMerchOrder
{
    public class CreateMerchOrderCommand:IRequest<int>
    {
        public long EmloyeeId { get; set; }

        public int MerchPackType { get; set; }
    }
}