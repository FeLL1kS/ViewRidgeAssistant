using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VRA.DataAccess.Entities;
using System.Data.SqlClient;

namespace VRA.DataAccess
{
    class CustomerArtistINTDao : BaseDao, ICustomerArtistINTDao
    {
        public void Add(CustomerArtistINT customerArtistINT)
        {
            using(var conn = GetConnection())
            {
                conn.Open();
                using(var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Customer_Artist_INT(ArtistID, CustomerID) VALUES(@ArtistID, @CustomerID)";
                    cmd.Parameters.AddWithValue("@ArtistID", customerArtistINT.ArtistID);
                    cmd.Parameters.AddWithValue("@CustomerID", customerArtistINT.CustomerID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int artistID, int customerID)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Customer_Artist_INT WHERE ArtistID = @ArtistID and CustomerID = @CustomerID";
                    cmd.Parameters.AddWithValue("@ArtistID", artistID);
                    cmd.Parameters.AddWithValue("@CustomerID", customerID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public CustomerArtistINT Get(int artistID, int customerID)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT ArtistID, CustomerID FROM Customer_Artist_INT WHERE ArtistID = @ArtistID and CustomerID = @CustomerID";
                    cmd.Parameters.AddWithValue("@ArtistID", artistID);
                    cmd.Parameters.AddWithValue("@CustomerID", customerID);
                    using(var dataReader = cmd.ExecuteReader())
                    {
                        return dataReader.Read() ? LoadCustomerArtistINT(dataReader) : null;
                    }
                }
            }
        }

        public IList<CustomerArtistINT> GetAll()
        {
            IList<CustomerArtistINT> customerArtistINTs = new List<CustomerArtistINT>();
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT ArtistID, CustomerID FROM Customer_Artist_INT";
                    using(var dataReader = cmd.ExecuteReader())
                    {
                        while(dataReader.Read())
                        {
                            customerArtistINTs.Add(LoadCustomerArtistINT(dataReader));
                        }
                        return customerArtistINTs;
                    }
                }
            }
        }

        public CustomerArtistINT LoadCustomerArtistINT(SqlDataReader dataReader)
        {
            CustomerArtistINT customerArtistINT = new CustomerArtistINT()
            {
                ArtistID = dataReader.GetInt32(dataReader.GetOrdinal("ArtistID")),
                CustomerID = dataReader.GetInt32(dataReader.GetOrdinal("CustomerID"))
            };
            return customerArtistINT;
        }
    }
}
