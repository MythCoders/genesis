using System.ComponentModel.DataAnnotations.Schema;

namespace eSIS.Core.Entities.Infrastructure
{
    [Table("ApiClient", Schema = "inf")]
    public class ApiClient : BaseEntity
    {
        public string Name { get; set; }
        public string Token { get; set; }
        public bool IsActive { get; set; }
    }
}