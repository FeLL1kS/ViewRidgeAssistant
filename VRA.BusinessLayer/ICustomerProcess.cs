using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VRA.Dto;

namespace VRA.BusinessLayer
{
    public interface ICustomerProcess
    {
        IList<CustomerDto> GetList();
        CustomerDto Get(int id);
        void Add(CustomerDto customer);
        void Update(CustomerDto customer);
        void Delete(int id);
        IList<CustomerDto> SearchCustomer(string Name);
    }
}
