using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VRA.BusinessLayer.Converters;
using VRA.DataAccess;
using VRA.Dto;

namespace VRA.BusinessLayer
{
    public class WorkProcessDb : IWorkProcess
    {
        private readonly IWorkDao _workDao;

        public WorkProcessDb()
        {
            _workDao = DaoFactory.GetWorkDao();
        }

        public void Add(WorkDto work)
        {
            _workDao.Add(DtoConverter.Convert(work));
        }

        public void Delete(int id)
        {
            _workDao.Delete(id);
        }

        public WorkDto Get(int id)
        {
            return DtoConverter.Convert((_workDao.Get(id)));
        }

        public IList<WorkDto> GetList()
        {
            return DtoConverter.Convert((_workDao.GetAll()));
        }

        public void Update(WorkDto work)
        {
            _workDao.Update(DtoConverter.Convert(work));
        }

        public IList<WorkDto> GetListInGallery()
        {
            return DtoConverter.Convert(_workDao.GetInGallery().ToList());
        }
    }
}
