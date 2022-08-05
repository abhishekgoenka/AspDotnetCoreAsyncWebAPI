using System;

namespace DotNetCoreAsysnSample.Infrastructure.Exceptions
{
    /// <summary>
    ///     Exception type for app exceptions
    /// </summary>
    public class CustomerDomainException : Exception
    {
        public CustomerDomainException()
        {
        }

        public CustomerDomainException(string message)
            : base(message)
        {
        }

        public CustomerDomainException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}