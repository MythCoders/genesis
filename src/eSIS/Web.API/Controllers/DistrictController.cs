using System.Web.Http;
using eSIS.Database.Core.Entities;
using eSIS.DB.DatabaseAccess;

namespace eSIS.Web.API.Controllers
{
    public class DistrictController : ApiController
    {
        private readonly DistrictRepository _repo;

        public DistrictController()
        {
            //_repo = new DistrictRepository();
        }

        public District Get(int id)
        {
            return new District
            { 
                ShortCode = "CCS",
                Address = new Address
                {
                    
                },
                Name = "Columbus City Schools"
            };
        }
    }
}
