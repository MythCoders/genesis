using eSIS.Core.Entities;
using eSIS.Core.UI;

namespace eSIS.Web.UI.Areas.Administration.Controllers
{
    public class SchoolController : UiControllerCrudBaseWithPaging<School>
    {
        public SchoolController()
            : base("adm/school", "SchoolController")
        {
            
        }
    }
}