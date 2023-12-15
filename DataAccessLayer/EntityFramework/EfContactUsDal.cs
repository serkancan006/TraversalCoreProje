using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfContactUsDal : GenericRepository<ContactUs>, IContactUsDal
    {
        public List<ContactUs> GetListContactUsByFalse()
        {
            using (var c = new Context())
            {
                return c.ContactUses.Where(x => x.Status == false).ToList();
            }
        }

        public List<ContactUs> GetListContactUsByTrue()
        {
            using(var c = new Context())
            {
                return c.ContactUses.Where(x => x.Status == true).ToList();
            }
        }

        public void GetListContactUsChangeStat(int id)
        {
            throw new NotImplementedException();
        }
    }
}
