using System;
using System.Collections.Generic;
using VRA.DataAccess.Entities;
using System.Data.SqlClient;

namespace VRA.DataAccess
{
    public class WorkInGalleryDao : BaseDao, IWorkInGalleryDao
    {
        public IList<WorkInGallery> GetAll()
        {
            IList<WorkInGallery> works = new List<WorkInGallery>();
            using(var conn = GetConnection())
            {
                conn.Open();
                using(var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT WorkID, Title, Copy, Name, AskingPrice, Description FROM WorksInGallery";
                    using(var dataReader = cmd.ExecuteReader())
                    {
                        while(dataReader.Read())
                        {
                            works.Add(LoadWork(dataReader));
                        }
                        return works;
                    }
                }
            }
        }

        public WorkInGallery LoadWork(SqlDataReader dataReader)
        {
            WorkInGallery work = new WorkInGallery
            {
                WorkID = dataReader.GetInt32(dataReader.GetOrdinal("WorkID")),
                Copy = dataReader.GetString(dataReader.GetOrdinal("Copy")),
                Description = dataReader.GetString(dataReader.GetOrdinal("Description")),
                Name = dataReader.GetString(dataReader.GetOrdinal("Name")),
                Title = dataReader.GetString(dataReader.GetOrdinal("Title"))
            };
            object AskingPrice = dataReader["AskingPrice"];
            if(AskingPrice != DBNull.Value)
            {
                work.AskingPrice = Convert.ToDecimal(AskingPrice);
            }
            return work;
        }
    }
}
