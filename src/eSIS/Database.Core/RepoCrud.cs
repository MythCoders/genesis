using System.Collections.Generic;
using System.Linq;
using eSIS.Database.Core.Entities;

namespace eSIS.Database.Core
{
    public abstract class RepoCrud<T, TT>
        where T : BaseEntity
        where TT : CustomDbContext
    {
        public readonly TT Database;
        public string UserName { get { return Database.UserName; } set { Database.UserName = value; } }
        public string UserIpAddress { get { return Database.UserIpAddress; } set { Database.UserIpAddress = value; } }

        protected RepoCrud(TT context)
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
