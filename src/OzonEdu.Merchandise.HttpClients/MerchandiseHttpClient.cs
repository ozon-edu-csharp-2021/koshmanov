using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using OzonEdu.Merchandise.Models;
using OzonEdu.Merchandise.RestEase;
using RestEase.HttpClientFactory;

namespace OzonEdu.Merchandise.HttpClients
{
    
    public class MerchandiseHttpClient:IMerchandiseHttpClient
    {
        private readonly HttpClient _client;
        
        public MerchandiseHttpClient(HttpClient client, IServiceCollection services)
        {
            _client = client;
            services.AddRestEaseClient<IMerchandiseHttpClient>("http://localhost:5000/v1/api/merch");
        }
        
        public async Task<GetMerchResponse> GetMerch(long employeeId, string itemName, CancellationToken token)
        {
            using var response = await _client.GetAsync($"v1/api/merch/{employeeId}/{itemName}", token);
            var body = await response.Content.ReadAsStringAsync(token);
            return JsonSerializer.Deserialize<GetMerchResponse>(body);
        }
        
        public async Task<GetOrderStateResponse> GetMerchGetMerchOrderState(long id, CancellationToken token)
        {
            using var response = await _client.GetAsync($"v1/api/merch/{id}", token);
            var body = await response.Content.ReadAsStringAsync(token);
            return JsonSerializer.Deserialize<GetOrderStateResponse>(body);
        }
    }
    
    
}