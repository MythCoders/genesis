using System.Collections.Generic;

namespace eSIS.Core.Classes
{
    public class JsonReturnValueData
    {
        public JsonReturnValueData()
        {
            Messages = new List<string>();
        }

        public bool Success { get; set; }
        public List<string> Messages { get; set; }
        public object[] Objects { get; set; }
    }
}