using System.Web.Http;
using MC.eSIS.Core.API;
using MC.eSIS.Core.Entities.Infrastructure;

namespace MC.eSIS.Web.API.Controllers
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