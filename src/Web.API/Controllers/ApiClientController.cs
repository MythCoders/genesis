using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;
using eSIS.Core.API;
using eSIS.Core.Entities.Infrastructure;

namespace eSIS.Web.API.Controllers
{
    [RoutePrefix("api/inf/apiclient")]
    public class ApiClientController : ServiceCrudBase<ApiClient>
    {
        public ApiClientController()
            : base("ApiClientService")
        {

        }

        [Route("{token}")]
        public async Task<IHttpActionResult> GetByToken(Guid token)
        {
            var item = await Database.ApiClients.SingleOrDefaultAsync(p => p.Token == token.ToString());

            if (item == null)
            {
                Logger.Debug("Item with token {0} was not found", token);
                return NotFound();
            }

            return Ok(item);
        }
    }
}