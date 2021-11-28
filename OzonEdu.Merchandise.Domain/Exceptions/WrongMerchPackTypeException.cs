using System;

namespace OzonEdu.Merchandise.Domain.Exceptions
{
    public class WrongMerchPackTypeException: Exception
    {
        public WrongMerchPackTypeException(string message):base(message)
        { }
    }
}