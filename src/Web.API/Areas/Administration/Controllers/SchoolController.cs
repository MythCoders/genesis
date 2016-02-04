using eSIS.Core.API;
using eSIS.Core.Entities;

namespace eSIS.Web.API.Areas.Administration.Controllers
{
    public class SchoolController : ServiceCrudBase<School>
    {
        public SchoolController()
            : base("SchoolService")
        {
            
        }
    }
}