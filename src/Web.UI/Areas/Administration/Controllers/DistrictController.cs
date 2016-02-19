using MC.eSIS.Core.Entities;
using MC.eSIS.Core.UI;

namespace MC.eSIS.Web.UI.Areas.Administration.Controllers
{
    public class DistrictController : UiControllerCrudBaseWithPaging<District>
    {
        public DistrictController() 
            : base("adm/district", "DistrictController")
        { }
    }
}