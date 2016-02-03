using System;
using System.Collections.Generic;
using eSIS.Core.Classes;

namespace eSIS.Core.Exceptions
{
    public class ValidationException : Exception
    {
        public List<PropertyError> PropertyErrors { get; set; }

        public ValidationException(List<PropertyError> errors)
        {
            PropertyErrors = errors;
        }

        public ValidationException(string message)
            : base(message)
        {
            PropertyErrors = new List<PropertyError>();
        }
    }
}
