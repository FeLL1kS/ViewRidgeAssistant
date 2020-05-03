using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VRA.BusinessLayer.Converters;
using VRA.DataAccess;
using VRA.Dto;

namespace VRA.BusinessLayer
{
    public class WorkInGalleryProcessDb : IWorkInGalleryProcess
    {
        private readonly IWorkInGalleryDao _workInGalleryDao;
        public WorkInGalleryProcessDb()
        {
            _workInGalleryDao = DaoFactory.GetWorkInGalleryDao();
        }
        public IList<WorkInGalleryDto> GetAll()
        {
            return DtoConverter.Convert(_workInGalleryDao.GetAll());
        }
    }
}
