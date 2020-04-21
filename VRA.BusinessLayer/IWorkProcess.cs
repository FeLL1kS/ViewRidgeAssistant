using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VRA.Dto;

namespace VRA.BusinessLayer
{
    public interface IWorkProcess
    {
        IList<WorkDto> GetList();
        WorkDto Get(int id);
        void Add(WorkDto work);
        void Update(WorkDto work);
        void Delete(int id);
    }
}
