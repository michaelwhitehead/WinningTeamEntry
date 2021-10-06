using System;
using System.Net;

namespace Template.Helpers.Exceptions
{
    public class ServerException : Exception
    {
        public ServerException()
        {
        }

        public ServerException(string message)
            : base(message)
        {
        }

        public ServerException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public Uri Uri { get; set; }

        public HttpStatusCode HttpStatusCode { get; set; }

        public string Content { get; set; }
    }
}
