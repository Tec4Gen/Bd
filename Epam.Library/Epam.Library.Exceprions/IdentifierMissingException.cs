using System;

namespace Epam.Library.Exceprions
{
    public class IdentifierMissingException : Exception
    {
        public IdentifierMissingException() : base() { }
        public IdentifierMissingException(string message) : base(message) { }
        public IdentifierMissingException(string message, Exception innerException) : base(message, innerException) { }
    }
}
