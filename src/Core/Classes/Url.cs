using System;
using System.Collections.Generic;
using System.Text;

// ReSharper disable UnusedMember.Global

namespace MC.eSIS.Core.Classes
{
    public class Url
    {
        private readonly string _baseUrl;
        private readonly List<string> _subDirectories;
        private string _method;

        public Url()
        {
            _baseUrl = ConfigurationHelper.InstanceApiBaseUrl();
            _subDirectories = new List<string>();
        }

        public Url(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public Url SubDirectory(string directoryName)
        {
            if (string.IsNullOrWhiteSpace(directoryName))
            {
                throw new ArgumentException("Directory Name is required", nameof(directoryName));
            }

            _subDirectories.Add(directoryName);
            return this;
        }

        public Url Method(string methodName)
        {
            if (string.IsNullOrWhiteSpace(methodName))
            {
                throw new ArgumentException("Method Name is required", nameof(methodName));
            }

            _method = methodName;
            return this;
        }

        public string Generate()
        {
            var sb = new StringBuilder();

            sb.Append(_baseUrl);

            foreach (var directory in _subDirectories)
            {
                sb.Append($"{directory}/");
            }

            sb.Append(_method);

            return sb.ToString();
        }
    }
}