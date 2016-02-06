using System.Web.Http;
using eSIS.Core.API;
using eSIS.Core.Entities;

namespace eSIS.Web.API.Controllers
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
