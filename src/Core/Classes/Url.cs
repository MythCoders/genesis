using System.Collections.Generic;
using System.Text;

namespace eSIS.Core.Classes
{
    internal class Url
    {
        public Url(string baseUrl)
        {
            BaseUrl = baseUrl;
            SubDirectories = new List<string>();
        }

        public string BaseUrl { get; set; }
        public string Method { get; set; }
        public List<string> SubDirectories { get; set; }

        public string GeneratePath()
        {
            var sb = new StringBuilder();

            sb.Append(BaseUrl);

            foreach (var directory in SubDirectories)
            {
                sb.Append($"{directory}/");
            }

            sb.Append(Method);

            return sb.ToString();
        }
    }
}
