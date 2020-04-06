using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VRA.DataAccess
{
    public class DaoFactory
    {
        public static IArtistDao GetArtistDao()
        {
            return new ArtistDao();
        }
        public static SettingsDao GetSettingsDao()
        {
            return new SettingsDao();
        }
        public static INationDao GetNationDao()
        {
            return new NationDao();
        }
    }
}
