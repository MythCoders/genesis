using eSIS.Core.API;
using eSIS.Core.Entities;

namespace eSIS.Web.API.Areas.Administration.Controllers
{
    public class EnrollmentCodeController : ServiceCrudBase<EnrollmentCode>
    {
        public EnrollmentCodeController() 
            : base("EnrollmentService")
        {
        }
    }
}