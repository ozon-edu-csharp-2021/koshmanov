using System.Collections.Generic;
using System.Linq;
using OzonEdu.Merchandise.Domain.Models;

namespace OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate
{
    public class MerchPackType : Enumeration
    {
        public static readonly MerchPackType WelcomePack  =              new MerchPackType(10, "WelcomePack");
        public static readonly MerchPackType ConferenceListenerPack  =   new MerchPackType(20, "ConferenceListenerPack");
        public static readonly MerchPackType ConferenceSpeakerPack  =    new MerchPackType(30, "ConferenceSpeakerPack");
        public static readonly MerchPackType ProbationPeriodEndingPack = new MerchPackType(40, "ProbationPeriodEndingPack");
        public static readonly MerchPackType VeteranPack  =              new MerchPackType(50, "VeteranPack");

        private MerchPackType(int id, string name) : base(id, name)
        {

        }
    }
}