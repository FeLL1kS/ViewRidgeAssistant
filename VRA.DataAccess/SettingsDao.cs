using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;

namespace VRA.DataAccess
{
    public class SettingsDao : ISettingsDao
    {
        public string GetSettings()
        {
            try
            {
                return ConfigurationManager.ConnectionStrings["vradb"].ConnectionString;
            }
            catch
            {
                return null;
            }
        }
        public bool SetSettings(string server, string db, string user, string password)
        {
            SqlConnectionStringBuilder conStr = new SqlConnectionStringBuilder();
            conStr.DataSource = server;
            conStr.InitialCatalog = db;
            if (user == "")
            {
                conStr.IntegratedSecurity = true;
            }
            else
            {
                conStr.UserID = user;
                conStr.Password = password;
            }
            try
            {
                SqlConnection con = new SqlConnection(conStr.ConnectionString);
                con.Open();
            }
            catch
            {
                return false;
            }
            Configuration config =
            ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.ConnectionStrings.ConnectionStrings["vradb"].ConnectionString =
            conStr.ConnectionString;
            config.Save();
            ConfigurationManager.RefreshSection("connectionStrings");
            config = ConfigurationManager.OpenExeConfiguration("VRA.exe");
            config.ConnectionStrings.ConnectionStrings["vradb"].ConnectionString =
            conStr.ConnectionString;
            config.Save();
            ConfigurationManager.RefreshSection("connectionStrings");
            return true;
        }
    }
}
