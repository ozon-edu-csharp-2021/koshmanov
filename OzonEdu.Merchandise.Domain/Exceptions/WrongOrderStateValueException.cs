using System;

namespace OzonEdu.Merchandise.Domain.Exceptions
{
    public class WrongOrderStateValueException : Exception
    {
        public WrongOrderStateValueException(string message):base(message)
        {
            
        }
    }
}