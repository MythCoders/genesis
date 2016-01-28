using System;
using System.Configuration;

namespace eSIS.Core.Classes
{
    public class UrlBuilder
    {
        private readonly Url _url;

        public UrlBuilder()
        {
            var baseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];
            _url = new Url(baseUrl);
        }

        public UrlBuilder(string baseUrl)
        {
            _url = new Url(baseUrl);
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

        public string Generate()
        {
            return _url.GeneratePath();
        }
    }
}