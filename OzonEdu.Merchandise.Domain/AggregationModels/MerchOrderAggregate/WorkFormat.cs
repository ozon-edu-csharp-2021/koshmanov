using OzonEdu.Merchandise.Domain.Models;

namespace OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate
{
    public class WorkFormat:Enumeration
    {
        public static readonly WorkFormat Remotely = new WorkFormat(1, "Remotely");
        public static readonly WorkFormat Local = new WorkFormat(1, "Local");
        public static readonly WorkFormat Hybrid = new WorkFormat(1, "Hybrid");
        public WorkFormat(int id, string name) : base(id, name)
        {
        }
    }
}