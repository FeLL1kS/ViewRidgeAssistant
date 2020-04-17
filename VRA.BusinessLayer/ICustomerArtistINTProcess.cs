using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VRA.Dto;

namespace VRA.BusinessLayer
{
    public interface ICustomerArtistINTProcess
    {
        IList<CustomerArtistINTDto> GetList();
        void Add(CustomerArtistINTDto customerArtistINT);
        void Delete(int artistID, int customerID);
    }
}
