using System.Web.Http;
using MC.eSIS.Core.API;
using MC.eSIS.Core.Entities;

namespace MC.eSIS.Web.API.Controllers
{
    [RoutePrefix("api/adm/schooltype")]
    public class SchoolTypeController : ServiceCrudBase<SchoolType>
    {
        public SchoolTypeController()
            : base("SchoolTypeService")
        {
            
        }
    }
}