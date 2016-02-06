using System.Web.Http;
using eSIS.Core.API;
using eSIS.Core.Entities.Infrastructure;

namespace eSIS.Web.API.Controllers
{
    [RoutePrefix("api/inf/log")]
    public class LogController : ServiceCrudBase<Log>
    {
        public LogController()
            : base("LogService")
        {
            
        }
    }
}