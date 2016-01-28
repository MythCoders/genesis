using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using eSIS.Core.Entities;
using eSIS.Database;

namespace eSIS.Core.API
{
    public class ServiceCrudBase<T> : ApiController
        where T : BaseEntity
    {
        public readonly SisContext Database;

        public ServiceCrudBase()
        {
            var userName = string.Empty;
            var ipAddress = string.Empty;

            //if (Request.Properties.ContainsKey("MS_HttpContext"))
            //{
            //    var ctx = Request.Properties["MS_HttpContext"] as HttpContextWrapper;
            //    if (ctx != null)
            //    {
            //        ipAddress = ctx.Request.UserHostAddress;
            //        userName = ctx.Request.UserHostName;
            //    }
            //}

            Database = new SisContext(userName, ipAddress);
        }

        [HttpGet]
        public virtual async Task<List<T>> GetAll()
        {
            return await Database.Set<T>().ToListAsync();
        }

        [HttpGet]
        public virtual async Task<IHttpActionResult> Get(int id)
        {
            var item = await Database.Set<T>().FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpGet]
        public virtual async Task<IHttpActionResult> GetBySystemCode(string code)
        {
            var item = await Database.Set<T>().SingleOrDefaultAsync(p => p.SystemCode == code);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPut]
        public virtual async Task<IHttpActionResult> Put(int id, T item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != item.Id)
            {
                return BadRequest();
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

        [HttpPost]
        public virtual async Task<IHttpActionResult> Post(T item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            Database.Set<T>().Add(item);
            await Database.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = item.Id }, item);
        }

        [HttpDelete]
        public virtual async Task<IHttpActionResult> Delete(int id)
        {
            var item = Database.Set<T>().Find(id);

            if (item == null)
            {
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
                Database.Dispose();
            }

            base.Dispose(disposing);
        }

        private bool Exists(int id)
        {
            return Database.Set<T>().Count(e => e.Id == id) > 0;
        }
    }
}
