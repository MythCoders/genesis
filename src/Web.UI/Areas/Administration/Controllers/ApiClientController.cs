using eSIS.Core.Entities.Infrastructure;
using eSIS.Core.UI;

namespace eSIS.Web.UI.Areas.Administration.Controllers
{
    public class ApiClientController : UiControllerCrudBaseWithPaging<ApiClient>
    {
        public ApiClientController()
            : base("adm/ApiClient", "ApiClient")
        {
            
        }
    }
}