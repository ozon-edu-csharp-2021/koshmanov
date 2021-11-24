using System.Collections.Generic;

namespace OzonEdu.Merchandise.Infrastructure.Repositories.Models
{
    public class MerchPackDto
    {
        public long Id { get; set; }

        public long Sku { get; set; }
        
        public int PackType { get; set; }
    }
}