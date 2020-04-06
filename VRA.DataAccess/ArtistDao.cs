using System;
using System.Collections.Generic;
using VRA.DataAccess.Entities;
using System.Data.SqlClient;

namespace VRA.DataAccess
{
    class ArtistDao : BaseDao, IArtistDao
    {
        public void Add(Artist artist)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO ARTIST(Name, BirthYear, DeceaseYear, NatID) VALUES(@Name, @BirthYear, @DeceaseYear, @Nationality)";
                    cmd.Parameters.AddWithValue("@Name", artist.Name);
                    cmd.Parameters.AddWithValue("@BirthYear", artist.BirthYear);
                    cmd.Parameters.AddWithValue("@Nationality", artist.NationId);
                    object decease = artist.DeceaseYear.HasValue ? (object)artist.DeceaseYear.Value : DBNull.Value;
                    cmd.Parameters.AddWithValue("@DeceaseYear", decease);
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
                    cmd.CommandText = "DELETE FROM ARTIST WHERE ArtistID = @ID";
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Artist Get(int id)
        {
            using (var conn = GetConnection())
            {
                //Открываем соединение
                conn.Open();
                //Создаем sql команду
                using (var cmd = conn.CreateCommand())
                {
                    //Задаём текст команды
                    cmd.CommandText = "SELECT ArtistID, Name, BirthYear, DeceaseYear, NatID FROM ARTIST WHERE ArtistID = @id";
                    //Добавляем значение параметра
                    cmd.Parameters.AddWithValue("@id", id);
                    //Открываем SqlDataReader для чтения полученных в результате
                    //выполнения запроса данных
                    using (var dataReader = cmd.ExecuteReader())
                    {
                        //Если есть запись, то работаем с ней
                        return dataReader.Read() ? LoadArtist(dataReader) : null;
                    }
                }
            }
        }

        public IList<Artist> GetAll()
        {
            IList<Artist> artists = new List<Artist>();
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT ArtistID, Name, BirthYear, DeceaseYear, NatID FROM ARTIST";
                    using (var dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            artists.Add(LoadArtist(dataReader));
                        }
                    }
                }
            }
            return artists;
        }

        public void Update(Artist artist)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "UPDATE ARTIST SET Name = @Name, BirthYear = @BirthYear, DeceaseYear = @DeceaseYear, NatID = @Nationality WHERE ArtistID = @ID";
                    cmd.Parameters.AddWithValue("@Name", artist.Name);
                    cmd.Parameters.AddWithValue("@BirthYear", artist.BirthYear);
                    cmd.Parameters.AddWithValue("@ID", artist.ArtistId);
                    cmd.Parameters.AddWithValue("@Nationality", artist.NationId);
                    object decease = artist.DeceaseYear.HasValue ? (object)artist.DeceaseYear.Value : DBNull.Value;
                    cmd.Parameters.AddWithValue("@DeceaseYear", decease);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        private static Artist LoadArtist(SqlDataReader reader)
        {
            //Создаём пустой объект
            Artist artist = new Artist();
            //Заполняем поля объекта в соответствии с названиями
            //полей результирующего набора данных
            artist.ArtistId = reader.GetInt32(reader.GetOrdinal("ArtistID"));
            artist.BirthYear = Convert.ToInt32(reader["BirthYear"]);
            //Помните, что у нас поле DeceaseYear может иметь значение NULL?
            //Следующие 3 строки корректно обрабатывают этот случай
            object decease = reader["DeceaseYear"];
            if (decease != DBNull.Value)
                artist.DeceaseYear = Convert.ToInt32(decease);
            artist.Name = reader.GetString(reader.GetOrdinal("Name"));
            int pos = reader.GetOrdinal("NatID");
            artist.NationId = reader[pos] == DBNull.Value ? -1 : reader.GetInt32(pos);
            return artist;
        }
    }
}
