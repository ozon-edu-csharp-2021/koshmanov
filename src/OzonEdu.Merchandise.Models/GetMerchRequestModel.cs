using System;
using System.Collections.Generic;

namespace OzonEdu.Merchandise.Models
{
  	
//    HTTP API
//    Проектируем два метода "запросить мерч" и "получить информацию о выдаче мерча".
//    Модели request и response помещаем в отдельный неймспейс.
//    В отдельный проект *.HttpClient выносим код клиента, который вызывает проработанные эндпоинты.
//
//    Реализация бизнес логики не требуется, поэтому во всех написанных эндпоинтах можно
//    выбрасывать NotImplementedException или возвращать мокированные данные.
//    public class GetMerchRequestModel
    public class GetMerchRequestModel
    {
        public string Employee;
    }
    public class GetMerchRequestModelV2
    {
        public Employee Employee;
    }

    public class GetMerchOrderStateResponseModel
    {
        public string Status { get; }
        public long Id { get; }
        public GetMerchOrderStateResponseModel(long id, string status)
        {
            Id = id;
            Status = status;
        }
    }
    public class GetMerchOrderStateResponseModelV2
    {
        public MerchOrderStatus Status;
    }

    public class GetMerchOrderStateRequestModel
    {
        public GetMerchOrderStateRequestModel(string order)
        {
            Order = order;
        }

        public string Order;
    }
    
    
    public class GetMerchOrderStateRequestModelV2
    {
        public MerchOrder Order;
    }
    
    public class GetMerchResponseModel
    {

        public string Order { get; }

        public GetMerchResponseModel(string order)
        {
            Order = order;
        }
    }
    public class GetMerchResponseModelV2
    {
        public MerchOrder Order;
    }
    
    public sealed class Employee
    {
        public long Id { get; }
        public string Name { get; }

        public Employee(long id, string name)
        {
            Id = id;
            Name = name;
        }
    }
    
    public sealed class MerchOrder
    {
        public long Id { get; }

        public MerchOrderStatus Status;
        public List<MerchItem> MerchItems { get; }

        public MerchOrder(long id, List<MerchItem> items)
        {
            Id = id;
            MerchItems = items;
            Status = MerchOrderStatus.Begin;
        }
        
        
    }

    public sealed class MerchItem
    {
        public string Name { get; }

        public MerchItem(string name)
        {
            Name = name;
        }
    }

    public enum MerchOrderStatus
    {
        Begin,
        End
    }
}