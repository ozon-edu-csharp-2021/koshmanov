using System;
using System.Collections.Generic;
using System.Linq;
using OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate;
using OzonEdu.Merchandise.Domain.Contracts;
using OzonEdu.Merchandise.Domain.Models;

namespace OzonEdu.Merchandise.Domain.AggregationModels.MerchPackAggregate
{
    public class MerchPack:Entity, IAggregateRoot
    {
        public IReadOnlyCollection<Sku> SkuCollection { get; private set; }
        public MerchPackType Type { get; }
        
        private MerchPack(long id, IReadOnlyCollection<Sku> merchItems, MerchPackType merchPackType)
        {
            Id = id;
            SkuCollection = merchItems;
            Type = merchPackType;
        }

        public static MerchPack Create(long id, IReadOnlyCollection<Sku> merchItems, MerchPackType merchPackType)
        {
            return new MerchPack(id, merchItems,  merchPackType);
        }

        public void AddToMerchPack( IReadOnlyCollection<Sku> skus)
        {
            var intersectArr =  SkuCollection
                .Select(x=>x.Value)
                .Intersect(skus.Select(y=>y.Value).ToArray()).ToArray();
            if ( intersectArr.Length == skus.Count)
            {
                throw new Exception($"Skus {string.Join(',',skus)} already exist");
            }

            SkuCollection = SkuCollection.Union(skus).ToArray();
        }
        
        public void DeleteFromMerchPack( IReadOnlyCollection<Sku> skus)
        {
            var intersectArr =  SkuCollection
                .Select(x=>x.Value)
                .Intersect(skus.Select(y=>y.Value).ToArray()).ToArray();
            if (! intersectArr.Any())
            {
                throw new Exception($"Skus {string.Join(',',skus)} not exist");
            }

            SkuCollection = SkuCollection.Except(skus).ToArray();
        }
    }
}