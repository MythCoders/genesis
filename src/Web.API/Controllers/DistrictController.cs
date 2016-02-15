using System.Web.Http;
using eSIS.Core.API;
using eSIS.Core.Entities;

namespace eSIS.Web.API.Controllers
{
    [RoutePrefix("api/adm/district")]
    public class DistrictController : ServiceCrudBase<District>
    {
        public DistrictController() 
            : base("DistrictService")
        {
        }

        public override void UpdateMapping(District existingItem, District updatedItem)
        {
            existingItem.SystemCode = updatedItem.SystemCode;
            existingItem.Name = updatedItem.Name;
            existingItem.Id = updatedItem.Id;
            existingItem.AddressId = updatedItem.AddressId;

            if (existingItem.Address != null)
            {
                existingItem.Address.Id = updatedItem.Address.Id;
                existingItem.Address.FirstAddressLine = updatedItem.Address.FirstAddressLine;
                existingItem.Address.SecondAddressLine = updatedItem.Address.SecondAddressLine;
                existingItem.Address.City = updatedItem.Address.City;
                existingItem.Address.Region = updatedItem.Address.Region;
                existingItem.Address.PostalCode = updatedItem.Address.PostalCode;
                existingItem.Address.County = updatedItem.Address.County;
                existingItem.Address.PhoneNumber = updatedItem.Address.PhoneNumber;
                existingItem.Address.AlternatePhoneNumber = updatedItem.Address.AlternatePhoneNumber;
                existingItem.Address.EmailAddress = updatedItem.Address.EmailAddress;
                existingItem.Address.AlternateEmailAddress = updatedItem.Address.AlternateEmailAddress;
            }
        }
    }
}
