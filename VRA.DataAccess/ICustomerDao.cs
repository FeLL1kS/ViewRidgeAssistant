using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VRA.DataAccess.Entities;

namespace VRA.DataAccess
{
    public interface ICustomerDao
    {
        Customer Get(int id);
        IList<Customer> GetAll();
        void Add(Customer customer);
        void Update(Customer customer);
        void Delete(int id);
    }
}
