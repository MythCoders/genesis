using System.Linq;
using eSIS.Core.API;
using eSIS.Core.Entities;

namespace eSIS.Web.API.Controllers
{
    public class DistrictController : ServiceCrudBase<District>
    {
        public override IQueryable<District> GetAll()
        {
            var district = new District
            {
                SystemCode = "PRT",
                Name = "Pathfinder Regional Tech",
                Id = 1
            };
            var returnDate = new[] {district};
            return new EnumerableQuery<District>(returnDate);
        }
    }
}
