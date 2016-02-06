using System.Web.Http;
using eSIS.Core.API;
using eSIS.Core.Entities;

namespace eSIS.Web.API.Controllers
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