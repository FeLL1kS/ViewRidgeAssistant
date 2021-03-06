﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VRA.BusinessLayer
{
    public class ProcessFactory
    {
        /// <summary>
        /// Возвращает объект, реализующий <seealso cref="IArtistProcess"/>
        /// </summary>
        /// <returns></returns>
        public static IArtistProcess GetArtistProcess()
        {
            return new ArtistProcessDb();
        }
        public static ISettingsProcess GetSettingsProcess()
        {
            return new SettingsProcess();
        }
        public static INationProcess GetNationProcess()
        {
            return new NationProcess();
        }

        public static ICustomerProcess GetCustomerProcess()
        {
            return new CustomerProcessDb();
        }

        public static ICustomerArtistINTProcess GetCustomerArtistINTProcess()
        {
            return new CustomerArtistINTProcessDb();
        }

        public static IWorkProcess GetWorkProcess()
        {
            return new WorkProcessDb();
        }

        public static ITransactionProcess GetTransactionProcess()
        {
            return new TransactionProcessDb();
        }

        public static IReportGenerator GetReportGenerator()
        {
            return new ReportGenerator();
        }

        public static IWorkInGalleryProcess GetWorkInGalleryProcess()
        {
            return new WorkInGalleryProcessDb();
        }

        public static IReportItemProcess GetReportProcess()
        {
            return new ReportItemProcess();
        }
    }
}
