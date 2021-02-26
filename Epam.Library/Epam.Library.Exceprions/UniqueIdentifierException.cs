using System;

namespace Epam.Library.Exceprions
{
    public class UniqueIdentifierException: Exception 
    {
        public UniqueIdentifierException() : base() { }
        public UniqueIdentifierException(string message) : base(message) { }
        public UniqueIdentifierException(string message, Exception innerException) : base(message, innerException) { }
    }
}
