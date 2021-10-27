using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.Merchandise.Models;

namespace OzonEdu.Merchandise.HttpClients
{
    public interface IMerchandiseHttpClient
    {
        
    }
    
    public class MerchandiseHttpClient:IMerchandiseHttpClient
    {
        private readonly HttpClient _client;

        public MerchandiseHttpClient(HttpClient client)
        {
            _client = client;
        }
        
        public async Task<GetMerchResponseModel> GetMerch(CancellationToken token)
        {
            using var response = await _client.GetAsync("v1/api/merch", token);
            var body = await response.Content.ReadAsStringAsync(token);
            return JsonSerializer.Deserialize<GetMerchResponseModel>(body);
        }
        
        public async Task<GetMerchOrderStateResponseModel> GetMerchGetMerchOrderState(long id,
            CancellationToken token)
        {
            using var response = await _client.GetAsync($"v1/api/merch/{id}", token);
            var body = await response.Content.ReadAsStringAsync(token);
            return JsonSerializer.Deserialize<GetMerchOrderStateResponseModel>(body);
        }
    }
    
    
}