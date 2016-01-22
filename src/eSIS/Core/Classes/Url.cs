using System.Collections.Generic;
using System.Text;

namespace eSIS.Core.Classes
{
    public class Url
    {
        public Url()
        {
            SubDirectories = new List<string>();
            Parameters = new Dictionary<string, object>();
        }

        public Url(string authorizationToken)
            : this()
        {
            AuthorizationToken = authorizationToken;
        }

        public string AuthorizationToken { get; set; }
        public string Method { get; set; }
        public List<string> SubDirectories { get; set; }
        public Dictionary<string, object> Parameters { get; set; }

        public string GeneratePath()
        {
            var sb = new StringBuilder();

            foreach (var directory in SubDirectories)
            {
                sb.Append($"{directory}/");
            }

            sb.Append(Method);

            return sb.ToString();
        }
    }
}
