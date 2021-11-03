using OzonEdu.Merchandise.Domain.Models;

namespace OzonEdu.Merchandise.Domain.AggregationModels.EmployeeAggregate
{
    public class Position:Enumeration
    {
        public static readonly Position Hr = new Position(1, "HR");
        public static readonly Position Manager = new Position(1, "Manager");
        public static readonly Position Developer = new Position(1, "Developer");
        public static readonly Position Analyst = new Position(1, "Analyst");
        public Position(int id, string name) : base(id, name)
        {
            
            
        }
    }
}