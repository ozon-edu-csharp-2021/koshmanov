using System.Collections.Generic;
using System.Collections.ObjectModel;
using MediatR;

namespace OzonEdu.Merchandise.Application.Commands.RecreateMerchOrder
{
    public class RecreateMerchOrderCommand:IRequest<List<long>>
    {
        public ICollection<StockReplenishedItem> Items { get; set; }
        
    }
    public class StockReplenishedItem
    {
        public long Sku { get; set; }

        public int ItemTypeId { get; set; }

        public string ItemTypeName { get; set; } = string.Empty;

        public int? ClothingSize { get; set; }
    }
}