using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VRA.Dto;

namespace VRA.BusinessLayer
{
    public interface ITransactionProcess
    {
        IList<TransactionDto> GetList();
        TransactionDto Get(int id);
        void Add(TransactionDto transaction);
        void Update(TransactionDto transaction);
        void Delete(int id);
        IList<TransactionDto> SearchTransaction(string CustomerID, string SalesPrice, DateTime? DateAcquiredFrom = null, DateTime? DateAcquiredTo = null);
    }
}
