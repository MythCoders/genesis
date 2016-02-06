using System.Web.Http;
using eSIS.Core.API;
using eSIS.Core.Entities;

namespace eSIS.Web.API.Controllers
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