using System.Web.Http;
using MC.eSIS.Core.API;
using MC.eSIS.Core.Entities;

namespace MC.eSIS.Web.API.Controllers
{
    [RoutePrefix("api/adm/district")]
    public class DistrictController : ServiceCrudBase<District>
    {
        public DistrictController() 
            : base("DistrictService")
        {
        }
    }
}
