using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using OzonEdu.Merchandise.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.Merchandise.Domain.Contracts;
using OzonEdu.Merchandise.Infrastructure.Repositories.Infrastructure.Interfaces;

namespace OzonEdu.Merchandise.Infrastructure.Repositories.Implementation
{
    public class MerchPackRepository:IMerchPackRepository
    {
        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
        private readonly IChangeTracker _changeTracker;
        private const int Timeout = 5;
        public IUnitOfWork UnitOfWork { get; }
        
        public MerchPackRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory, IChangeTracker changeTracker)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _changeTracker = changeTracker;
        }

        public async Task<IReadOnlyList<MerchPack>> GetPackListByMerchTypeIdAsync(int merchPackTypeId, CancellationToken cancellationToken=default)
        {
            string sql=@$"
                        select merch_pack_id, sku, type_id
                        from merch_pack_sku_map    
                        where type_id = {merchPackTypeId}";
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var result = await connection.QueryAsync<Models.MerchPackDto>(sql);
            
            var merchPackList = new List<MerchPack>(); 
            var idList = result.Select(x => x.Id).Distinct().ToList();
            var packIdSkuPair = new Dictionary<long, List<long>>();
            idList.ForEach(x =>
            {
                packIdSkuPair.Add(x, result.Where(y=>y.Id==x).Select(y => y.Sku).ToList());
            });
            foreach (var keyValuePair in packIdSkuPair)
            {
                var skuList = new List<Sku>();
                keyValuePair.Value.ForEach(x=>skuList.Add(new Sku(x)));
                merchPackList.Add(MerchPack.Create(keyValuePair.Key, skuList, MerchPackType.Parse(merchPackTypeId)));
            }
            return merchPackList;
        }

        public async Task<MerchPack> GetPackByIdAsync(long packId, CancellationToken cancellationToken = default)
        {
            string sql=@$"
                        select merch_pack_id, sku, type_id
                        from merch_pack_sku_map    
                        where merch_pack_id = {packId}";
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var result = await connection.QueryAsync<Models.MerchPackDto>(sql);
            
            var packTypeId = result.First().PackType;
            List<long> resSkus = result.Select(x => x.Sku).ToList();
            List<Sku> skuList = new List<Sku>();
            resSkus.ForEach(x=>skuList.Add(new Sku(x)));
            return  MerchPack.Create(packId, skuList, MerchPackType.Parse(packTypeId));
        }
    }
}