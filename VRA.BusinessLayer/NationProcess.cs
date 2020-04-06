using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VRA.Dto;
using VRA.BusinessLayer.Converters;
using VRA.DataAccess;

namespace VRA.BusinessLayer
{
    public class NationProcess : INationProcess
    {
        public static INationDao NatDao = new NationDao();

        public void Add(NationDto n)
        {
            NatDao.Add(DtoConverter.Convert(n));
        }

        public void Delete(int id)
        {
            NatDao.Delete(id);
        }

        public IList<NationDto> GetList()
        {
            return DtoConverter.Convert(DaoFactory.GetNationDao().Load());
        }
    }
}
