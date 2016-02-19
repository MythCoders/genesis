using System.Web.Http;
using MC.eSIS.Core.API;
using MC.eSIS.Core.Entities.Infrastructure;

namespace MC.eSIS.Web.API.Controllers
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