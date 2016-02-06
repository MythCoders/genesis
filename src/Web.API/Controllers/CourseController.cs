using System.Web.Http;
using eSIS.Core.API;
using eSIS.Core.Entities;

namespace eSIS.Web.API.Controllers
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