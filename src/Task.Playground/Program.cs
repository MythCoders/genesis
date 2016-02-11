using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using eSIS.Core;
using eSIS.Core.Classes;
using eSIS.Core.UI;
using Kendo.Mvc.UI;
using Newtonsoft.Json;

namespace eSIS.Task.Playground
{
    class Program
    {
        static void Main(string[] args)
        {
            Dude();
            Console.ReadLine();
        }

        private async static void Dude()
        {
            var request = new DataSourceRequest { PageSize = 10, Page = 1 };
            var client = new WebApiClient();

            var url = new Url().SubDirectory("adm/district").Method("page").Generate();
            var data = await client.MakeRawPostRequest(url, request);

            Debugger.Break();

            var jsonStr = Encoding.UTF8.GetString(data);


            
            var foo = JsonConvert.DeserializeObject<DataSourceResult>(jsonStr, ConfigurationHelper.GetJsonSerializerSettings());

            Debugger.Break();
        }
    }
}
