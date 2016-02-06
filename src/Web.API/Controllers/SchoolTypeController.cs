using System.Web.Http;
using eSIS.Core.API;
using eSIS.Core.Entities;

namespace eSIS.Web.API.Controllers
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