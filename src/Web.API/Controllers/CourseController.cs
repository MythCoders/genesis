using System.Web.Http;
using MC.eSIS.Core.API;
using MC.eSIS.Core.Entities;

namespace MC.eSIS.Web.API.Controllers
{
    [RoutePrefix("api/adm/course")]
    public class CourseController : ServiceCrudBase<Course>
    {
        public CourseController()
            : base("CourseService")
        {
            
        }
    }
}