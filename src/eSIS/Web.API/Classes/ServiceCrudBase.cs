using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using eSIS.Core.Entities;
using eSIS.Database;

namespace eSIS.Web.API.Classes
{
    public class ServiceCrudBase<T> : ApiController
        where T : BaseEntity
    {
        public readonly SisContext Database;

        public ServiceCrudBase()
        {
            var userName = string.Empty;
            var ipAddress = string.Empty;

            if (Request.Properties.ContainsKey("MS_HttpContext"))
            {
                var ctx = Request.Properties["MS_HttpContext"] as HttpContextWrapper;
                if (ctx != null)
                {
                    ipAddress = ctx.Request.UserHostAddress;
                    userName = ctx.Request.UserHostName;
                }
            }

            Database = new SisContext(userName, ipAddress);
        }

        [HttpGet]
        public IQueryable<T> GetAll()
        {
            return Database.Set<T>();
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var item = Database.Set<T>().Find(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpGet]
        public IHttpActionResult GetBySystemCode(string code)
        {
            var item = Database.Set<T>().SingleOrDefault(p => p.SystemCode == code);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPut]
        public IHttpActionResult Put(int id, T item)
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
                Database.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(id))
                {
                    return NotFound();
                }

                throw;
            }
            catch (Exception ex)
            {
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        public IHttpActionResult Post(T item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            Database.Set<T>().Add(item);
            Database.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = item.Id }, item);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var item = Database.Set<T>().Find(id);

            if (item == null)
            {
                return NotFound();
            }

            Database.Set<T>().Remove(item);
            Database.SaveChanges();

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
