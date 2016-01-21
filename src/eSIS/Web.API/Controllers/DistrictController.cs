using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using eSIS.Database;
using eSIS.Database.Entities;

namespace eSIS.Web.API.Controllers
{
    public class DistrictController : ApiController
    {
        private SisContext db = new SisContext();

        // GET: api/District
        public IQueryable<District> GetDistricts()
        {
            return db.Districts;
        }

        // GET: api/District/5
        [ResponseType(typeof(District))]
        public IHttpActionResult GetDistrict(int id)
        {
            District district = db.Districts.Find(id);
            if (district == null)
            {
                return NotFound();
            }

            return Ok(district);
        }

        // PUT: api/District/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDistrict(int id, District district)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != district.Id)
            {
                return BadRequest();
            }

            db.Entry(district).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DistrictExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/District
        [ResponseType(typeof(District))]
        public IHttpActionResult PostDistrict(District district)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Districts.Add(district);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = district.Id }, district);
        }

        // DELETE: api/District/5
        [ResponseType(typeof(District))]
        public IHttpActionResult DeleteDistrict(int id)
        {
            District district = db.Districts.Find(id);
            if (district == null)
            {
                return NotFound();
            }

            db.Districts.Remove(district);
            db.SaveChanges();

            return Ok(district);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DistrictExists(int id)
        {
            return db.Districts.Count(e => e.Id == id) > 0;
        }
    }
}