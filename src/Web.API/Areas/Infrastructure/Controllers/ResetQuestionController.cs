using System;
using eSIS.Core.API;
using eSIS.Core.Entities.Infrastructure;
using NLog;

namespace eSIS.Web.API.Areas.Infrastructure.Controllers
{
    public class ResetQuestionController : ServiceCrudBase<ResetQuestion>
    {
        public ResetQuestionController() 
            : base("ResetQuestionService")
        {
        }
    }
}