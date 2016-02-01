using System;

namespace eSIS.Core.API.Exceptions
{
    public class RequiredHeaderException : Exception
    {
        public RequiredHeaderException(string message)
            : base(message)
        {

        }
    }
}