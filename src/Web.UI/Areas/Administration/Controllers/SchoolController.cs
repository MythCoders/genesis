using MC.eSIS.Core.Entities;
using MC.eSIS.Core.UI;

namespace MC.eSIS.Web.UI.Areas.Administration.Controllers
{
    public class SchoolController : UiControllerCrudBaseWithPaging<School>
    {
        public SchoolController()
            : base("adm/school", "SchoolController")
        {
            
        }
    }
}