using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using eSIS.Core.API.Security;
using eSIS.Core.Entities;
using eSIS.Database;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using NLog;

namespace eSIS.Core.API
{
    [ApiAuthentication]
    [Authorize]
    public class ServiceCrudBase<T> : ApiController
        where T : BaseEntity
    {
        // ReSharper disable MemberCanBePrivate.Global
        public readonly SisContext Database;
        public Logger Logger;
        // ReSharper restore MemberCanBePrivate.Global

        public ServiceCrudBase(string serviceName)
        {
            Logger = LogManager.GetLogger(serviceName);
            Logger.Trace("Service starting");


            var userName = "TestUser"; //User.Identity.Name;
            var ipAddress = HttpContext.Current.Request.UserHostAddress;
            Database = new SisContext(userName, ipAddress);
        }

        [Route("")]
        public IHttpActionResult Get()
        {
            var data = Database.Set<T>().ToList();
            return Ok(data);
        }

        /// <summary>
        /// Is this my documentation?
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("Page")]
        public IHttpActionResult Page(DataSourceRequest request)
        {
            if (request == null)
            {
                Logger.Debug("Request was null");
                return BadRequest("Request was null");
            }

            var data = Database.Set<T>().ToDataSourceResult(request);
            return Ok(data);
        }

        [Route("{id:int}")]
        // ReSharper disable once VirtualMemberNeverOverriden.Global
        public virtual async Task<IHttpActionResult> Get(int id)
        {
            var item = await Database.Set<T>().FindAsync(id);

            if (item == null)
            {
                Logger.Debug("Item with id {0} was not found", id);
                return NotFound();
            }

            return Ok(item);
        }

        [Route("{code}")]
        // ReSharper disable once VirtualMemberNeverOverriden.Global
        public virtual async Task<IHttpActionResult> GetBySystemCode(string code)
        {
            var item = await Database.Set<T>().SingleOrDefaultAsync(p => p.SystemCode == code);

            if (item == null)
            {
                Logger.Debug("Item with code {0} was not found", code);
                return NotFound();
            }

            return Ok(item);
        }

        [Route("{id:int}")]
        // ReSharper disable once VirtualMemberNeverOverriden.Global
        public virtual async Task<IHttpActionResult> Put(int id, T item)
        {
            if (id != item.Id)
            {
                Logger.Debug("Ids mismatched Bad Request");
                return BadRequest("Id mis-match");
            }

            //PreUpdateValidation();

            if (!ModelState.IsValid)
            {
                Logger.Debug("ModelState not valid. Bad Request");
                return BadRequest(ModelState);
            }

            Database.Entry(item).State = EntityState.Modified;

            try
            {
                await Database.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("")]
        // ReSharper disable once VirtualMemberNeverOverriden.Global
        public virtual async Task<IHttpActionResult> Post(T item)
        {
            //PreCreateValidation();

            if (!ModelState.IsValid)
            {
                Logger.Debug("ModelState not valid. Bad Request");
                return BadRequest(ModelState);
            }

            Database.Set<T>().Add(item);

            await Database.SaveChangesAsync();

            return CreatedAtRoute("Default", new { id = item.Id }, item);
        }

        [Route("{id:int}")]
        // ReSharper disable once VirtualMemberNeverOverriden.Global
        public virtual async Task<IHttpActionResult> Delete(int id)
        {
            var item = Database.Set<T>().Find(id);

            if (item == null)
            {
                Logger.Debug("Not found id={0}", id);
                return NotFound();
            }

            Database.Set<T>().Remove(item);
            await Database.SaveChangesAsync();

            return Ok(item);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Logger.Trace("Disposting context");
                Database.Dispose();
            }

            base.Dispose(disposing);
        }

        private bool Exists(int id)
        {
            return Database.Set<T>().Count(e => e.Id == id) > 0;
        }

        // ReSharper disable once VirtualMemberNeverOverriden.Global
        //public virtual void PreCreateValidation()
        //{
        //    Logger.Trace("No pre-create validation found");
        //}

        // ReSharper disable once VirtualMemberNeverOverriden.Global
        //public virtual void PreUpdateValidation()
        //{
        //    Logger.Trace("No pre-update validation found");
        //}
    }
}
