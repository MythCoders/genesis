using System;

namespace eSIS.Core.Classes
{
    public class UrlBuilder
    {
        private readonly Url _url;

        public UrlBuilder()
        {
            _url = new Url();
        }

        public UrlBuilder(string authoirzationToken)
        {
            _url = new Url(authoirzationToken);
        }

        public UrlBuilder SubDirectory(string directoryName)
        {
            if (string.IsNullOrWhiteSpace(directoryName))
            {
                throw new ArgumentException("Directory Name is required", nameof(directoryName));
            }

            _url.SubDirectories.Add(directoryName);
            return this;
        }

        public UrlBuilder Method(string methodName)
        {
            if (string.IsNullOrWhiteSpace(methodName))
            {
                throw new ArgumentException("Method Name is required", nameof(methodName));
            }

            _url.Method = methodName;
            return this;
        }

        public UrlBuilder Parameter(string name, string value)
        {
            _url.Parameters.Add(name, value);
            return this;
        }

        public Url Generate()
        {
            return _url;
        }
    }
}