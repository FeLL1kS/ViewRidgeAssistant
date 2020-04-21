using System;
using System.Collections.Generic;
using VRA.DataAccess.Entities;
using System.Data.SqlClient;

namespace VRA.DataAccess
{
    class WorkDao : BaseDao, IWorkDao
    {
        public void Add(Work work)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Work(Title, Copy, Description, ArtistID) VALUES(@Title, @Copy, @Description, @ArtistID)";
                    cmd.Parameters.AddWithValue("@Title", work.Title);
                    cmd.Parameters.AddWithValue("@Copy", work.Copy);
                    cmd.Parameters.AddWithValue("@Description", work.Description);
                    cmd.Parameters.AddWithValue("@ArtistID", work.ArtistID);
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
                    cmd.CommandText = "DELETE FROM Work WHERE WorkID = @ID";
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Work Get(int id)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT WorkID, ArtistID, Title, Copy, Description FROM Work WHERE ArtistID = @ID";
                    cmd.Parameters.AddWithValue("@ID", id);
                    using (var dataReader = cmd.ExecuteReader())
                    {
                        return dataReader.Read() ? LoadWork(dataReader) : null;
                    }
                }
            }
        }

        public IList<Work> GetAll()
        {
            IList<Work> works = new List<Work>();
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT WorkID, ArtistID, Title, Copy, Description FROM Work";
                    using (var dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            works.Add(LoadWork(dataReader));
                        }
                    }
                }
            }
            return works;
        }

        public void Update(Work work)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "UPDATE Work SET ArtistID = @ArtistID, Title = @Title, Copy = @Copy, Description = @Description WHERE WorkID = @ID";
                    cmd.Parameters.AddWithValue("@Title", work.Title);
                    cmd.Parameters.AddWithValue("@Copy", work.Copy);
                    cmd.Parameters.AddWithValue("@Description", work.Description);
                    cmd.Parameters.AddWithValue("@ArtistID", work.ArtistID);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        private static Work LoadWork(SqlDataReader dataReader)
        {
            Work work = new Work()
            {
                WorkID = dataReader.GetInt32(dataReader.GetOrdinal("WorkID")),
                ArtistID = dataReader.GetInt32(dataReader.GetOrdinal("ArtistID")),
                Title = dataReader.GetString(dataReader.GetOrdinal("Title")),
                Copy = dataReader.GetString(dataReader.GetOrdinal("Copy")),
                Description = dataReader.GetString(dataReader.GetOrdinal("Description"))
            };
            return work;
        }
    }
}
