using System.Collections.Generic;
using MediatR;
using OzonEdu.Merchandise.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate;

namespace OzonEdu.Merchandise.Infrastructure.Commands.CreateMerchOrder
{
    public class CreateMerchOrderCommand:IRequest<int>
    {
        public Employee Emloyee { get; set; }
        public List<MerchItem> ItemList { get; set; }
        
        public MerchManager MerchManager { get; set; }
    }
}