using MC.eSIS.Core.Entities.Infrastructure;
using MC.eSIS.Core.UI;

namespace MC.eSIS.Web.UI.Areas.Administration.Controllers
{
    public class ApiClientController : UiControllerCrudBaseWithPaging<ApiClient>
    {
        public ApiClientController()
            : base("adm/ApiClient", "ApiClient")
        {
            
        }
    }
}