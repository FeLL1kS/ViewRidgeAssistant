using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VRA.DataAccess.Entities;
using System.Data.SqlClient;

namespace VRA.DataAccess
{
    public class ReportDao : BaseDao, IReport
    {
        private static Report LoadReport(SqlDataReader reader)
        {
            //Создаём пустой объект
            Report report = new Report();
            //Заполняем поля объекта в соответствии с названиями полей результирующего
            // набора данных
            try
            {
                report.Date = reader.GetDateTime(reader.GetOrdinal("mydate"));
            }
            catch (Exception ex)
            {
                report.Date = DateTime.Now.Date;
            }
            report.Price = reader.GetDecimal(reader.GetOrdinal("mysum"));
            report.Count = reader.GetInt32(reader.GetOrdinal("mycount"));
            return report;
        }

        public IList<Report> getPurchasedPerDays(DateTime start, DateTime end)
        {
            IList<Report> reports = new List<Report>();
            //Получаем объект подключения к базе
            using (var conn = GetConnection())
            {
                //Открываем соединение
                conn.Open();
                //Создаем sql команду
                using (var cmd = conn.CreateCommand())
                {
                    //Задаём текст команды
                    cmd.CommandText = "select CONVERT(date, DateAcquired, 105) as mydate, isnull(SUM(AcquisitionPrice), 0.0) as mysum, ISNULL(count(AcquisitionPrice), 0.0) as mycount from TRANS where DateAcquired between @start and @stop group by CONVERT(date, DateAcquired, 105)";
                    //Добавляем значение параметра
                    cmd.Parameters.AddWithValue("@start", start);
                    cmd.Parameters.AddWithValue("@stop", end);
                    //Открываем SqlDataReader для чтения полученных в результате
                    // выполнения запроса данных
                    using (var dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            reports.Add(LoadReport(dataReader));
                        }
                    }
                }
            }
            return reports;
        }

        public IList<Report> getPurchasedPerMonth(DateTime start, DateTime end)
        {
            IList<Report> reports = new List<Report>();
            //Получаем объект подключения к базе
            using (var conn = GetConnection())
            {
                //Открываем соединение
                conn.Open();
                //Создаем sql команду
                using (var cmd = conn.CreateCommand())
                {
                    //Задаём текст команды
                    cmd.CommandText = "select CONVERT(date, DateAcquired, 105) as mydate, isnull(SUM(AcquisitionPrice), 0.0) as mysum, ISNULL(count(AcquisitionPrice), 0.0) as mycount from TRANS where DateAcquired between @start and @stop group by CONVERT(date, DateAcquired, 105)";
                    //Добавляем значение параметра
                    cmd.Parameters.AddWithValue("@start", start);
                    cmd.Parameters.AddWithValue("@stop", end);
                    //Открываем SqlDataReader для чтения полученных в результате
                    // выполнения запроса данных
                    using (var dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            reports.Add(LoadReport(dataReader));
                        }
                    }
                }
            }
            return reports;
        }

        public IList<Report> getPurchasedPerYear(DateTime start, DateTime end)
        {
            IList<Report> reports = new List<Report>();
            //Получаем объект подключения к базе
            using (var conn = GetConnection())
            {
                //Открываем соединение
                conn.Open();
                //Создаем sql команду
                using (var cmd = conn.CreateCommand())
                {
                    //Задаём текст команды
                    cmd.CommandText = "select CONVERT(date, DateAcquired, 105) as mydate, isnull(SUM(AcquisitionPrice), 0.0) as mysum, ISNULL(count(AcquisitionPrice), 0.0)as mycount from TRANS where DATEPART(Year, DateAcquired) between DATEPART(Year, @start) and DATEPART(Year, @stop) group by CONVERT(date, DateAcquired, 105)";
                    //Добавляем значение параметра
                    cmd.Parameters.AddWithValue("@start", start);
                    cmd.Parameters.AddWithValue("@stop", end);
                    //Открываем SqlDataReader для чтения полученных в результате
                    // выполнения запроса данных
                    using (var dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            reports.Add(LoadReport(dataReader));
                        }
                    }
                }
            }
            return reports;
        }

        public IList<Report> getSalesPerDay(DateTime start, DateTime end)
        {
            IList<Report> reports = new List<Report>();
            //Получаем объект подключения к базе
            using (var conn = GetConnection())
            {
                //Открываем соединение
                conn.Open();
                //Создаем sql команду
                using (var cmd = conn.CreateCommand())
                {
                    //Задаём текст команды
                    cmd.CommandText = "select CONVERT(date, PurchaseDate, 105) as mydate, isnull(SUM(SalesPrice), 0.0) as mysum, ISNULL(count(SalesPrice), 0.0) as mycount from TRANS where PurchaseDate between @start and @stop group by CONVERT(date, PurchaseDate, 105)";
                    //Добавляем значение параметра
                    cmd.Parameters.AddWithValue("@start", start);
                    cmd.Parameters.AddWithValue("@stop", end);
                    //Открываем SqlDataReader для чтения полученных в результате
                    // выполнения запроса данных
                    using (var dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            reports.Add(LoadReport(dataReader));
                        }
                    }
                }
            }
            return reports;
        }

        public IList<Report> getSalesPerMonth(DateTime start, DateTime end)
        {
            IList<Report> reports = new List<Report>();
            //Получаем объект подключения к базе
            using (var conn = GetConnection())
            {
                //Открываем соединение
                conn.Open();
                //Создаем sql команду
                using (var cmd = conn.CreateCommand())
                {
                    //Задаём текст команды
                    cmd.CommandText = "select CONVERT(date, PurchaseDate, 105) as mydate, isnull(SUM(SalesPrice), 0.0) as mysum, ISNULL(count(SalesPrice), 0.0) as mycount from TRANS where PurchaseDate between @start and @stop group by CONVERT(date, PurchaseDate, 105)";
                    //Добавляем значение параметра
                    cmd.Parameters.AddWithValue("@start", start);
                    cmd.Parameters.AddWithValue("@stop", end);
                    //Открываем SqlDataReader для чтения полученных в результате
                    // выполнения запроса данных
                    using (var dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            reports.Add(LoadReport(dataReader));
                        }
                    }
                }
            }
            return reports;
        }

        public IList<Report> getSalesPerYear(DateTime start, DateTime end)
        {
            IList<Report> reports = new List<Report>();
            //Получаем объект подключения к базе
            using (var conn = GetConnection())
            {
                //Открываем соединение
                conn.Open();
                //Создаем sql команду
                using (var cmd = conn.CreateCommand())
                {
                    //Задаём текст команды
                    cmd.CommandText = "select CONVERT(date, PurchaseDate, 105) as mydate, isnull(SUM(SalesPrice), 0.0) as mysum, ISNULL(count(SalesPrice), 0.0) as mycount from TRANS where DATEPART(Year, DateAcquired) between DATEPART(Year, @start) and DATEPART(Year, @stop) group by CONVERT(date, PurchaseDate, 105)";
                    //Добавляем значение параметра
                    cmd.Parameters.AddWithValue("@start", start);
                    cmd.Parameters.AddWithValue("@stop", end);
                    //Открываем SqlDataReader для чтения полученных в результате
                    // выполнения запроса данных
                    using (var dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            reports.Add(LoadReport(dataReader));
                        }
                    }
                }
            }
            return reports;
        }
    }
}
