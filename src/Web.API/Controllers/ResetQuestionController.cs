using System.Web.Http;
using eSIS.Core.API;
using eSIS.Core.Entities.Infrastructure;

namespace eSIS.Web.API.Controllers
{
    [RoutePrefix("api/inf/resetquestion")]
    public class ResetQuestionController : ServiceCrudBase<ResetQuestion>
    {
        public ResetQuestionController() 
            : base("ResetQuestionService")
        {
        }
    }
}