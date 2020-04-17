using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VRA.BusinessLayer.Converters;
using VRA.DataAccess;
using VRA.Dto;

namespace VRA.BusinessLayer
{
    public class CustomerProcessDb : ICustomerProcess
    {
        private readonly ICustomerDao _customerDao;

        public CustomerProcessDb()
        {
            _customerDao = DaoFactory.GetCustomerDao();
        }

        public void Add(CustomerDto customer)
        {
            _customerDao.Add(DtoConverter.Convert(customer));
        }

        public void Delete(int id)
        {
            _customerDao.Delete(id);
        }

        public CustomerDto Get(int id)
        {
            return DtoConverter.Convert((_customerDao.Get(id)));
        }

        public IList<CustomerDto> GetList()
        {
            return DtoConverter.Convert((_customerDao.GetAll()));
        }

        public void Update(CustomerDto customer)
        {
            _customerDao.Update(DtoConverter.Convert(customer));
        }
    }
}
