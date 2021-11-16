using System;

namespace OzonEdu.Merchandise.Domain.Exceptions
{
    public class InvalidEmailException : Exception
    {
        public InvalidEmailException(string message) : base(message)
        { }

    }
}