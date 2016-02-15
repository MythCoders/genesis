using eSIS.Core.Entities;
using eSIS.Core.UI;

namespace eSIS.Web.UI.Areas.Administration.Controllers
{
    public class DistrictController : UiControllerCrudBaseWithPaging<District>
    {
        public DistrictController() 
            : base("adm/district", "District")
        { }
    }
}