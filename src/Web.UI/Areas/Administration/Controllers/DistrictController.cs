using eSIS.Core.Entities;
using eSIS.Core.UI;

namespace eSIS.Web.UI.Areas.Administration.Controllers
{
    public class DistrictController : UiControllerCrudBase<District>
    {
        public DistrictController() 
            : base("District")
        { }
    }
}