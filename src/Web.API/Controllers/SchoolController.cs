using System.Web.Http;
using MC.eSIS.Core.API;
using MC.eSIS.Core.Entities;

namespace MC.eSIS.Web.API.Controllers
{
    [RoutePrefix("api/adm/school")]
    public class SchoolController : ServiceCrudBase<School>
    {
        public SchoolController()
            : base("SchoolService")
        {
            
        }
    }
}