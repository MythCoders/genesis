using eSIS.Database.Core;
using eSIS.Database.Core.Entities;

namespace eSIS.DB.DatabaseAccess
{
    public class DistrictRepository : RepoCrud<District, SisContext>
    {
        public DistrictRepository() 
            : base(new SisContext())
        {

        }
    }
}