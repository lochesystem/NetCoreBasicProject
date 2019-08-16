using System;

namespace BasicProject.Domain.Exceptions
{
    public class BasicProjectException : Exception
    {
        public BasicProjectException()
        { }

        public BasicProjectException(string message) : base(message)
        { }

        public BasicProjectException(string message, Exception innerException) : base(message, innerException)
        { }

    }
}
