using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VRA.DataAccess.Entities;

namespace VRA.DataAccess
{
    public interface ICustomerArtistINTDao
    {
        CustomerArtistINT Get(int artistID, int customerID);
        IList<CustomerArtistINT> GetAll();
        void Add(CustomerArtistINT customerArtistINT);
        void Delete(int artistID, int customerID);
    }
}
