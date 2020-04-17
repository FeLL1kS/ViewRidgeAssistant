using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VRA.BusinessLayer.Converters;
using VRA.DataAccess;
using VRA.Dto;

namespace VRA.BusinessLayer
{
    public class CustomerArtistINTProcessDb : ICustomerArtistINTProcess
    {
        private readonly ICustomerArtistINTDao _customerArtistINT;

        public CustomerArtistINTProcessDb()
        {
            _customerArtistINT = DaoFactory.GetCustomerArtistINTDao();
        }

        public void Add(CustomerArtistINTDto customerArtistINT)
        {
            _customerArtistINT.Add(DtoConverter.Convert(customerArtistINT));
        }

        public void Delete(int artistID, int customerID)
        {
            _customerArtistINT.Delete(artistID, customerID);
        }

        public IList<CustomerArtistINTDto> GetList()
        {
            return DtoConverter.Convert(_customerArtistINT.GetAll());
        }
    }
}
