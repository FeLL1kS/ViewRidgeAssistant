using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VRA.BusinessLayer.Converters;
using VRA.DataAccess;
using VRA.Dto;

namespace VRA.BusinessLayer
{
    public class TransactionProcessDb : ITransactionProcess
    {
        private readonly ITransactionDao _transactionDao;

        public TransactionProcessDb()
        {
            _transactionDao = DaoFactory.GetTransactionDao();
        }

        public void Add(TransactionDto transaction)
        {
            _transactionDao.Add(DtoConverter.Convert(transaction));
        }

        public void Delete(int id)
        {
            _transactionDao.Delete(id);
        }

        public TransactionDto Get(int id)
        {
            return DtoConverter.Convert((_transactionDao.Get(id)));
        }

        public IList<TransactionDto> GetList()
        {
            return DtoConverter.Convert((_transactionDao.GetAll()));
        }

        public void Update(TransactionDto transaction)
        {
            _transactionDao.Update(DtoConverter.Convert(transaction));
        }
    }
}
