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
        public static ISettingsDao GetSettingsDao()
        {
            return new SettingsDao();
        }
        public static INationDao GetNationDao()
        {
            return new NationDao();
        }

        public static ICustomerDao GetCustomerDao()
        {
            return new CustomerDao();
        }

        public static ICustomerArtistINTDao GetCustomerArtistINTDao()
        {
            return new CustomerArtistINTDao();
        }

        public static IWorkDao GetWorkDao()
        {
            return new WorkDao();
        }

        public static ITransactionDao GetTransactionDao()
        {
            return new TransactionDao();
        }

        public static IWorkInGalleryDao GetWorkInGalleryDao()
        {
            return new WorkInGalleryDao();
        }

        public static IReport GetReport()
        {
            return new ReportDao();
        }
    }
}
