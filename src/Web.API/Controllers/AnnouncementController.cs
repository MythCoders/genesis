using System.Web.Http;
using eSIS.Core.API;
using eSIS.Core.Entities.Infrastructure;

namespace eSIS.Web.API.Controllers
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