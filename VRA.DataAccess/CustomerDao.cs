using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VRA.DataAccess.Entities;
using System.Data.SqlClient;

namespace VRA.DataAccess
{
    public class CustomerDao : BaseDao, ICustomerDao
    {
        public void Add(Customer customer)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Customer(E_mail, Name, AreaCode, HouseNumber, Street, City, Region, ZipPostalCode, Country, PhoneNumber) VALUES(@E_mail, @Name, @AreaCode, @HouseNumber, @Street, @City, @Region, @ZipPostalCode, @Country, @PhoneNumber)";
                    cmd.Parameters.AddWithValue("@E_mail", customer.EMail);
                    cmd.Parameters.AddWithValue("@Name", customer.Name);
                    cmd.Parameters.AddWithValue("@AreaCode", customer.AreaCode);
                    cmd.Parameters.AddWithValue("@HouseNumber", customer.HouseNumber);
                    cmd.Parameters.AddWithValue("@Street", customer.Street);
                    cmd.Parameters.AddWithValue("@City", customer.City);
                    cmd.Parameters.AddWithValue("@Region", customer.Region);
                    cmd.Parameters.AddWithValue("@ZipPostalCode", customer.ZipPostalCode);
                    cmd.Parameters.AddWithValue("@Country", customer.Country);
                    cmd.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using(var conn = GetConnection())
            {
                conn.Open();
                using(var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Customer WHERE CustomerID = @ID";
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Customer Get(int id)
        {
            using(var conn = GetConnection())
            {
                conn.Open();
                using(var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT CustomerID, E_mail, Name, AreaCode, HouseNumber, Street, City, Region, ZipPostalCode, Country, PhoneNumber FROM Customer WHERE CustomerID = @ID";
                    cmd.Parameters.AddWithValue("@ID", id);
                    using(var dataReader = cmd.ExecuteReader())
                    {
                        return dataReader.Read() ? LoadCustomer(dataReader) : null; 
                    }
                }
            }
        }

        public IList<Customer> GetAll()
        {
            IList<Customer> customers = new List<Customer>();
            using (var conn = GetConnection())
            {
                conn.Open();
                using(var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT CustomerID, E_mail, Name, AreaCode, HouseNumber, Street, City, Region, ZipPostalCode, Country, PhoneNumber FROM Customer";
                    using(var dataReader = cmd.ExecuteReader())
                    {
                        while(dataReader.Read())
                        {
                            customers.Add(LoadCustomer(dataReader));
                        }

                        return customers;
                    }
                }
            }
        }

        public void Update(Customer customer)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "UPDATE Customer SET E_mail = @E_mail, Name = @Name, AreaCode = @AreaCode, HouseNumber = @HouseNumber, Street = @Street, City = @City, Region = @Region, ZipPostalCode = @ZipPostalCode, Country = @Country, PhoneNumber = @PhoneNumber WHERE CustomerID = @ID";
                    cmd.Parameters.AddWithValue("@ID", customer.Id);
                    cmd.Parameters.AddWithValue("@E_mail", customer.EMail);
                    cmd.Parameters.AddWithValue("@Name", customer.Name);
                    cmd.Parameters.AddWithValue("@AreaCode", customer.AreaCode);
                    cmd.Parameters.AddWithValue("@HouseNumber", customer.HouseNumber);
                    cmd.Parameters.AddWithValue("@Street", customer.Street);
                    cmd.Parameters.AddWithValue("@City", customer.City);
                    cmd.Parameters.AddWithValue("@Region", customer.Region);
                    cmd.Parameters.AddWithValue("@ZipPostalCode", customer.ZipPostalCode);
                    cmd.Parameters.AddWithValue("@Country", customer.Country);
                    cmd.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static Customer LoadCustomer(SqlDataReader dataReader)
        {
            Customer customer = new Customer
            {
                Id = dataReader.GetInt32(dataReader.GetOrdinal("CustomerID")),
                EMail = dataReader.GetString(dataReader.GetOrdinal("E_mail")),
                Name = dataReader.GetString(dataReader.GetOrdinal("Name")),
                AreaCode = dataReader.GetString(dataReader.GetOrdinal("AreaCode")),
                HouseNumber = dataReader.GetString(dataReader.GetOrdinal("HouseNumber")),
                Street = dataReader.GetString(dataReader.GetOrdinal("Street")),
                City = dataReader.GetString(dataReader.GetOrdinal("City")),
                Region = dataReader.GetString(dataReader.GetOrdinal("Region")),
                ZipPostalCode = dataReader.GetString(dataReader.GetOrdinal("ZipPostalCode")),
                Country = dataReader.GetString(dataReader.GetOrdinal("Country")),
                PhoneNumber = dataReader.GetString(dataReader.GetOrdinal("PhoneNumber"))
            };

            return customer;
        }
    }
}
