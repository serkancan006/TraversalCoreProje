using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class ContactUsManager : IContactUsService
    {
        IContactUsDal _ContactUsDal;
        public ContactUsManager(IContactUsDal contactUsDal)
        {
            _ContactUsDal = contactUsDal;
        }

        public void TAdd(ContactUs t)
        {
            _ContactUsDal.Insert(t);
        }

        public void TDelete(ContactUs t)
        {
            _ContactUsDal.Delete(t);
        }

        public ContactUs TGetByID(int id)
        {
            return _ContactUsDal.GetByID(id);
        }

        public List<ContactUs> TGetList()
        {
            return _ContactUsDal.GetList();
        }

        public List<ContactUs> TGetListContactUsByFalse()
        {
            return _ContactUsDal.GetListContactUsByFalse();
        }

        public List<ContactUs> TGetListContactUsByTrue()
        {
            return _ContactUsDal.GetListContactUsByTrue();
        }

        public void TGetListContactUsChangeStat(int id)
        {
            throw new NotImplementedException();
        }

        public void TUpdate(ContactUs t)
        {
            _ContactUsDal.Update(t);
        }
    }
}
