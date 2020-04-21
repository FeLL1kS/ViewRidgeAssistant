using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VRA.DataAccess.Entities;

namespace VRA.DataAccess
{
    public interface IWorkDao
    {
        Work Get(int id);
        IList<Work> GetAll();
        void Add(Work work);
        void Update(Work work);
        void Delete(int id);
    }
}
