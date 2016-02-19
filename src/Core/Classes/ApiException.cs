using System;
using System.Net;

namespace MC.eSIS.Core.Classes
{
    public class ApiException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public ApiException(HttpStatusCode statusCode, string message)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}