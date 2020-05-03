using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VRA.DataAccess.Entities;

namespace VRA.DataAccess
{
    public interface IWorkInGalleryDao
    {
        IList<WorkInGallery> GetAll();
    }
}
