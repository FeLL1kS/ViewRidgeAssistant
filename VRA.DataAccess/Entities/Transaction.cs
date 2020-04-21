using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VRA.DataAccess.Entities
{
    public class Transaction
    {
        public int TransID;
        public int? CustomerID;
        public int WorkID;
        public DateTime DateAcquired;
        public decimal AcquisitionPrice;
        public DateTime? PurchaseDate;
        public decimal? SalesPrice;
        public decimal AskingPrice;
    }
}
