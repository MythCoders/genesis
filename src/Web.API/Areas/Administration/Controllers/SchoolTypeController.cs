using eSIS.Core.API;
using eSIS.Core.Entities;

namespace eSIS.Web.API.Areas.Administration.Controllers
{
    public class SchoolTypeController : ServiceCrudBase<SchoolType>
    {
        public SchoolTypeController()
            : base("SchoolTypeService")
        {
            
        }
    }
}