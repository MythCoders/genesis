using System.Web.Http;
using MC.eSIS.Core.API;
using MC.eSIS.Core.Entities;

namespace MC.eSIS.Web.API.Controllers
{
    [RoutePrefix("api/adm/enrollmentcode")]
    public class EnrollmentCodeController : ServiceCrudBase<EnrollmentCode>
    {
        public EnrollmentCodeController() 
            : base("EnrollmentService")
        {
        }
    }
}