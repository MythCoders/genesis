using System;
using System.Net;

namespace eSIS.Core.Exceptions
{
    public class ApiErrorExcpetion : Exception
    {
        /// <summary>
        /// Allows you to override the default http status code that is returned for this ApiError. 
        /// Overrides logic in <see cref="ApiExceptionHandler"/>
        /// </summary>
        public HttpStatusCode? OverridenHttpStatusCode { get; set; }

        /// <summary>
        /// An general summary of the error. The end user will most likely see this message
        /// </summary>
        public new string Message { get; set; }

        /// <summary>
        /// An optional, detailed, error message that might provide more information on what the problem is. 
        /// </summary>
        public string DetailedMessage { get; set; }

        /// <summary>
        /// Array of Objects that will be returned to the client as a Json object
        /// </summary>
        public object[] JsonObjects { get; set; }

        public ApiErrorExcpetion(string message) 
            : this(message, null)
        {
        }

        public ApiErrorExcpetion(string message, string detailedMessage) 
            : this(message, detailedMessage, new object[] { })
        {
        }

        public ApiErrorExcpetion(string message, string detailedMessage, object[] jsonObjects)
        {
            Message = message;
            DetailedMessage = detailedMessage;
            JsonObjects = jsonObjects;
        }
    }
}