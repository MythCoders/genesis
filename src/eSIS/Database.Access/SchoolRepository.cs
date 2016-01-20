using System.Collections.Generic;
using System.Linq;
using eSIS.Database.Core;
using eSIS.Database.Core.Entities;

namespace eSIS.DB.DatabaseAccess
{
    public class SchoolRepository : RepoCrud<School, SisContext>
    {
        public SchoolRepository()
            : base(new SisContext())
        {
            
        }

        public List<School> GetByDistrictId(int districtId)
        {
            return Database.Schools.Where(p => p.DistrictId == districtId).ToList();
        }
    }
}