using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;

namespace eSIS.Database
{
    [DbConfigurationType("eSIS.Database.SisContextConfiguration, eSIS.Database")]
    public class SisContextConfiguration : DbConfiguration
    {
        public SisContextConfiguration()
        {
            SetTransactionHandler(SqlProviderServices.ProviderInvariantName, () => new CommitFailureHandler());
        }
    }
}