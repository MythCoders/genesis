using System.Web.Http;
using MC.eSIS.Core.API;
using MC.eSIS.Core.Entities.Infrastructure;

namespace MC.eSIS.Web.API.Controllers
{
    [RoutePrefix("api/inf/announcement")]
    public class AnnouncementController : ServiceCrudBase<Announcement>
    {
        public AnnouncementController()
            : base("AnnouncementService")
        {
            
        }
    }
}