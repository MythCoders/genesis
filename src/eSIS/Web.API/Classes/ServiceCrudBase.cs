using System.Collections.Generic;
using System.Linq;
using eSIS.Database;
using eSIS.Database.Entities;

namespace eSIS.Web.API.Classes
{
    public abstract class ServiceCrudBase<T>
        where T : BaseEntity
    {
        public SisContext Database;

        public ServiceCrudBase(string userName, string ipAddress)
        {
            Database = new SisContext(userName, ipAddress);
        }

        public ServiceCrudBase(SisContext context)
        {
            Database = context;
        }

        public virtual int Create(T item)
        {
            Database.Set<T>().Add(item);
            Database.SaveChanges();
            return item.Id;
        }

        public virtual void Delete(int id)
        {
            var item = Get(id);
            Database.Set<T>().Remove(item);
            Database.SaveChanges();
        }

        public virtual void Update(T item)
        {
            //Mapper.CreateMap<T, T>();
            //var currentItem = Get(item.Id);
            //Mapper.Map(item, currentItem);
            Database.SaveChanges();
        }

        public virtual T Get(int id)
        {
            return Database.Set<T>().Find(id);
        }

        public virtual List<T> GetAll()
        {
            return Database.Set<T>().ToList();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
