using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VRA.DataAccess.Entities;

namespace VRA.DataAccess
{
    public interface ITransactionDao
    {
        Transaction Get(int id);
        IList<Transaction> GetAll();
        void Add(Transaction transaction);
        void Update(Transaction transaction);
        void Delete(int id);
    }
}
