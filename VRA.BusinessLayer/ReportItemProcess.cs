using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using VRA.BusinessLayer.Converters;
using VRA.DataAccess;
using VRA.Dto;

namespace VRA.BusinessLayer
{
    public class ReportItemProcess : IReportItemProcess
    {
        private static readonly IReport _reportDao = new ReportDao();
        private static ObservableCollection<ReportItemDto> GetCollection(IList<ReportItemDto> Items, string period, DateTime start, DateTime stop)
        {
            ObservableCollection<ReportItemDto> Collection = new ObservableCollection<ReportItemDto>();
            // Условие проверки наличия принятых данных.
            if (Items == null) { return null; }
            
            switch(period)
            {
                case "day":
                    DateTime d = start;
                    while (d <= stop)
                    {
                        ReportItemDto repItem = new ReportItemDto
                        {
                            Date = d.Date.ToString("dd.MM.yyyy"),
                            Count = 0,
                            Price = 0
                        };
                        foreach (var item in Items)
                        {
                            if (Convert.ToDateTime(item.Date).Date == d)
                            {
                                repItem.Count += item.Count;
                                repItem.Price += item.Price;
                            }
                        }
                        Collection.Add(repItem);
                        d = d.AddDays(1);
                    }
                    break;
                case "month":
                    DateTime dd = start;
                    while (dd <= stop)
                    {
                        ReportItemDto repItem = new ReportItemDto
                        {
                            Date = dd.Date.ToString("Y"),
                            Count = 0,
                            Price = 0
                        };
                        foreach (var item in Items)
                        {
                            if ((Convert.ToDateTime(item.Date).Month == dd.Month) && (Convert.ToDateTime(item.Date).Year == dd.Year))
                            {
                                repItem.Count += item.Count;
                                repItem.Price += item.Price;
                            }
                        }
                        Collection.Add(repItem);
                        dd = dd.AddMonths(1);
                    }
                    break;
                case "year":
                    DateTime ddd = start;
                    while (ddd <= stop)
                    {
                        ReportItemDto repItem = new ReportItemDto
                        {
                            Date = ddd.Date.Year.ToString(),
                            Count = 0,
                            Price = 0
                        };
                        foreach (var item in Items)
                        {
                            if (Convert.ToDateTime(item.Date).Year == ddd.Year)
                            {
                                repItem.Count += item.Count;
                                repItem.Price += item.Price;
                            }
                        }
                        Collection.Add(repItem);
                        ddd = ddd.AddYears(1);
                    }
                    break;
            }
            return Collection;
        }

        public ObservableCollection<ReportItemDto> GetPurchased(string period, DateTime start, DateTime stop)
        {
            IList<ReportItemDto> ReportList;
            switch (period)
            {
                case "day":
                    ReportList = DtoConverter.Convert(_reportDao.getPurchasedPerDays(start, stop));
                    break;
                case "month":
                    ReportList = DtoConverter.Convert(_reportDao.getPurchasedPerMonth(start, stop));
                    break;
                case "year":
                    ReportList = DtoConverter.Convert(_reportDao.getPurchasedPerYear(start, stop));
                    break;
                default:
                    ReportList = null;
                    break;
            }
            return GetCollection(ReportList, period, start, stop);
        }

        public ObservableCollection<ReportItemDto> GetSaled(string period, DateTime start, DateTime stop)
        {
            IList<ReportItemDto> ReportList;
            switch (period)
            {
                case "day":
                    ReportList = DtoConverter.Convert(_reportDao.getSalesPerDay(start, stop));
                    break;
                case "month":
                    ReportList = DtoConverter.Convert(_reportDao.getSalesPerMonth(start, stop));
                    break;
                case "year":
                    ReportList = DtoConverter.Convert(_reportDao.getSalesPerYear(start, stop));
                    break;
                default:
                    ReportList = null;
                    break;
            }
            return GetCollection(ReportList, period, start, stop);
        }
    }
}
