using System;
using System.Collections.Generic;
using VRA.DataAccess.Entities;
using System.Data.SqlClient;

namespace VRA.DataAccess
{
    class TransactionDao : BaseDao, ITransactionDao
    {
        public void Add(Transaction transaction)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Trans(CustomerID, WorkID, DateAcquired, AcquisitionPrice, PurchaseDate, SalesPrice, AskingPrice) VALUES(null, @WorkID, @DateAcquired, @AcquisitionPrice, @PurchaseDate, @SalesPrice, @AskingPrice)";
                    if(transaction.CustomerID != null)
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", transaction.CustomerID);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", DBNull.Value);
                    }
                    cmd.Parameters.AddWithValue("@WorkID", transaction.WorkID);
                    cmd.Parameters.AddWithValue("@DateAcquired", transaction.DateAcquired);
                    cmd.Parameters.AddWithValue("@AcquisitionPrice", transaction.AcquisitionPrice);
                    if(transaction.PurchaseDate != null)
                    {
                        cmd.Parameters.AddWithValue("@PurchaseDate", transaction.PurchaseDate);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@PurchaseDate", DBNull.Value);
                    }
                    if(transaction.SalesPrice != null)
                    {
                        cmd.Parameters.AddWithValue("@SalesPrice", transaction.SalesPrice);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@SalesPrice", DBNull.Value);
                    }
                    cmd.Parameters.AddWithValue("@AskingPrice", transaction.AskingPrice);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Trans WHERE TransID = @ID";
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Transaction Get(int id)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT TransID, CustomerID, WorkID, DateAcquired, AcquisitionPrice, PurchaseDate, SalesPrice, AskingPrice FROM Trans WHERE TransID = @ID";
                    cmd.Parameters.AddWithValue("@ID", id);
                    using (var dataReader = cmd.ExecuteReader())
                    {
                        return dataReader.Read() ? LoadTransaction(dataReader) : null;
                    }
                }
            }
        }

        public IList<Transaction> GetAll()
        {
            IList<Transaction> works = new List<Transaction>();
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT TransID, CustomerID, WorkID, DateAcquired, AcquisitionPrice, PurchaseDate, SalesPrice, AskingPrice FROM Trans";
                    using (var dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            works.Add(LoadTransaction(dataReader));
                        }
                    }
                }
            }
            return works;
        }

        public void Update(Transaction transaction)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "UPDATE Trans SET CustomerID = @CustomerID, WorkID = @WorkID, DateAcquired = @DateAcquired, AcquisitionPrice = @AcquisitionPrice, PurchaseDate = @PurchaseDate, SalesPrice = @SalesPrice, AskingPrice = @AskingPrice WHERE TransID = @ID";
                    cmd.Parameters.AddWithValue("@ID", transaction.TransID);
                    cmd.Parameters.AddWithValue("@CustomerID", transaction.CustomerID);
                    cmd.Parameters.AddWithValue("@WorkID", transaction.WorkID);
                    cmd.Parameters.AddWithValue("@DateAcquired", transaction.DateAcquired);
                    cmd.Parameters.AddWithValue("@AcquisitionPrice", transaction.AcquisitionPrice);
                    cmd.Parameters.AddWithValue("@PurchaseDate", transaction.PurchaseDate);
                    cmd.Parameters.AddWithValue("@SalesPrice", transaction.SalesPrice);
                    cmd.Parameters.AddWithValue("@AskingPrice", transaction.AskingPrice);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private static Transaction LoadTransaction(SqlDataReader dataReader)
        {
            Transaction transaction = new Transaction()
            {
                TransID = dataReader.GetInt32(dataReader.GetOrdinal("TransID")),
                WorkID = dataReader.GetInt32(dataReader.GetOrdinal("WorkID")),
                DateAcquired = dataReader.GetDateTime(dataReader.GetOrdinal("DateAcquired")),
                AcquisitionPrice = dataReader.GetDecimal(dataReader.GetOrdinal("AcquisitionPrice")),
                AskingPrice = dataReader.GetDecimal(dataReader.GetOrdinal("AskingPrice")),
            };
            object CustomerID = dataReader["CustomerID"];
            if (CustomerID != DBNull.Value)
                transaction.CustomerID = Convert.ToInt32(CustomerID);
            object PurchaseDate = dataReader["PurchaseDate"];
            if (PurchaseDate != DBNull.Value)
                transaction.PurchaseDate = dataReader.GetDateTime(dataReader.GetOrdinal("PurchaseDate"));
            object SalesPrice = dataReader["SalesPrice"];
            if (SalesPrice != DBNull.Value)
                transaction.SalesPrice = dataReader.GetDecimal(dataReader.GetOrdinal("SalesPrice"));
            return transaction;
        }
    }
}
