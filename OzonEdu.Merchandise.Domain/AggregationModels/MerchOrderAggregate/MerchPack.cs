using System.Collections.Generic;
using System.Linq;
using OzonEdu.Merchandise.Domain.AggregationModels.NamesAggregate;
using OzonEdu.Merchandise.Domain.Models;

namespace OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate
{
    public class MerchPack:Enumeration
    {
        public static readonly MerchPack WelcomePack = new MerchPack(new List<MerchItem>
        {
            new MerchItem( new MerchItemName("Bag"), new Sku(1)),
            new MerchItem( new MerchItemName("Pen"), new Sku(2)),
            new MerchItem( new MerchItemName("Notebook"), new Sku(3))
        }, MerchPackType.WelcomePack);
        public static readonly MerchPack ProbationPeriodEndingPack = new MerchPack(new List<MerchItem>
        {
            new MerchItem( new MerchItemName("Sweatshirt"), new Sku(11)),
            new MerchItem( new MerchItemName("Cup"), new Sku(12)),
        }, MerchPackType.ProbationPeriodEndingPack);
        public static readonly MerchPack ConferenceSpeakerPack = new MerchPack(new List<MerchItem>
        {
            new MerchItem( new MerchItemName("Umbrella"), new Sku(21)),
            new MerchItem( new MerchItemName("Сap"), new Sku(2)),
            new MerchItem( new MerchItemName("Phone case"), new Sku(32)),
        }, MerchPackType.ConferenceSpeakerPack);
        public static readonly MerchPack ConferenceListenerPack = new MerchPack(new List<MerchItem>
        {
            new MerchItem( new MerchItemName("Stickers"), new Sku(31)),
            new MerchItem( new MerchItemName("Phone case"), new Sku(32)),
        }, MerchPackType.ConferenceListenerPack);
        
        public static readonly MerchPack VeteranPack = new MerchPack(new List<MerchItem>
        {
            new MerchItem( new MerchItemName("MacBook"), new Sku(41)),

        }, MerchPackType.VeteranPack);
        
        private static readonly List<MerchPack> SearchList = new List<MerchPack>()
        {
            WelcomePack, ConferenceListenerPack, ConferenceSpeakerPack, ProbationPeriodEndingPack, VeteranPack
        };

        public ICollection<MerchItem> MerchItems { get; }
        public MerchPackType MerchPackType { get; }

        public MerchPack(ICollection<MerchItem> merchItems, MerchPackType merchPackType):base(merchPackType.Id, merchPackType.Name)
        {
            MerchItems = merchItems;
            MerchPackType = merchPackType;
        }

        public static bool TryGetPackById(int id, ref MerchPack packType)
        {
            packType = SearchList.FirstOrDefault(x => x.MerchPackType.Id.Equals(id));
            return packType != null;
        }
    }
}