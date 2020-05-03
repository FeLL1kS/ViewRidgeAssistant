using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VRA.Dto;
using VRA.DataAccess.Entities;
using VRA.DataAccess;

namespace VRA.BusinessLayer.Converters
{
    class DtoConverter
    {
        public static ArtistDto Convert(Artist artist)
        {
            if (artist == null)
                return null;
            ArtistDto artistDto = new ArtistDto();
            artistDto.Id = artist.ArtistId;
            artistDto.BirthYear = artist.BirthYear;
            artistDto.DeceaseYear = artist.DeceaseYear;
            artistDto.Name = artist.Name;
            artistDto.Nation = Convert(DaoFactory.GetNationDao().Get(artist.NationId));
            return artistDto;
        }
        public static Artist Convert(ArtistDto artistDto)
        {
            if (artistDto == null)
                return null;
            Artist artist = new Artist();
            artist.ArtistId = artistDto.Id;
            artist.BirthYear = artistDto.BirthYear;
            artist.DeceaseYear = artistDto.DeceaseYear;
            artist.Name = artistDto.Name;
            artist.NationId = artistDto.Nation.Id;
            return artist;
        }
        public static IList<ArtistDto> Convert(IList<Artist> artists)
        {
            if (artists == null)
                return null;
            IList<ArtistDto> artistDtos = new List<ArtistDto>();
            foreach (var artist in artists)
            {
                artistDtos.Add(Convert(artist));
            }
            return artistDtos;
        }
        public static NationDto Convert(Nation nation)
        {
            if (nation == null)
                return null;
            NationDto nationDto = new NationDto();
            nationDto.Id = nation.Id;
            nationDto.Nationality = nation.Name;
            return nationDto;
        }
        public static Nation Convert(NationDto nationDto)
        {
            if (nationDto == null)
                return null;
            Nation nation = new Nation();
            nation.Id = nationDto.Id;
            nation.Name = nationDto.Nationality;
            return nation;
        }
        internal static IList<NationDto> Convert(IList<Nation> nationList)
        {
            List<NationDto> nations = new List<NationDto>();
            foreach(Nation nation in nationList)
            {
                nations.Add(Convert(nation));
            }
            return nations;
        }
        public static CustomerDto Convert(Customer customer)
        {
            if (customer == null)
                return null;
            CustomerDto customerDto = new CustomerDto()
            {
                Id = customer.Id,
                AreaCode = customer.AreaCode,
                City = customer.City,
                Country = customer.Country,
                ZipPostalCode = customer.ZipPostalCode,
                EMail = customer.EMail,
                HouseNumber = customer.HouseNumber,
                Name = customer.Name,
                PhoneNumber = customer.PhoneNumber,
                Region = customer.Region,
                Street = customer.Street
            };
            return customerDto;
        }
        public static Customer Convert(CustomerDto customerDto)
        {
            if (customerDto == null)
                return null;
            Customer customer = new Customer()
            {
                Id = customerDto.Id,
                Name = customerDto.Name,
                City = customerDto.City,
                Country = customerDto.Country,
                AreaCode = customerDto.AreaCode,
                ZipPostalCode = customerDto.ZipPostalCode,
                EMail = customerDto.EMail,
                HouseNumber = customerDto.HouseNumber,
                PhoneNumber = customerDto.PhoneNumber,
                Region = customerDto.Region,
                Street = customerDto.Street
            };
            return customer;
        }
        internal static IList<CustomerDto> Convert(IList<Customer> customerList)
        {
            IList<CustomerDto> customerDtos = new List<CustomerDto>();
            foreach(Customer customer in customerList)
            {
                customerDtos.Add(Convert(customer));
            }
            return customerDtos;
        }
        public static CustomerArtistINTDto Convert(CustomerArtistINT customerArtistINT)
        {
            if (customerArtistINT == null)
                return null;
            CustomerArtistINTDto customerArtistINTDto = new CustomerArtistINTDto()
            {
                ArtistID = Convert(DaoFactory.GetArtistDao().Get(customerArtistINT.ArtistID)),
                CustomerID = Convert(DaoFactory.GetCustomerDao().Get(customerArtistINT.CustomerID))
            };
            return customerArtistINTDto;
        }
        public static CustomerArtistINT Convert(CustomerArtistINTDto customerArtistINTDto)
        {
            if (customerArtistINTDto == null)
                return null;
            CustomerArtistINT customerArtistINT = new CustomerArtistINT()
            {
                ArtistID = customerArtistINTDto.ArtistID.Id,
                CustomerID = customerArtistINTDto.CustomerID.Id
            };
            return customerArtistINT;
        }
        internal static IList<CustomerArtistINTDto> Convert(IList<CustomerArtistINT> customerArtistINTList)
        {
            IList<CustomerArtistINTDto> customerArtistINTDtos = new List<CustomerArtistINTDto>();
            foreach(CustomerArtistINT customerArtistINT in customerArtistINTList)
            {
                customerArtistINTDtos.Add(Convert(customerArtistINT));
            }
            return customerArtistINTDtos;
        }
        public static WorkDto Convert(Work work)
        {
            if (work == null)
                return null;
            WorkDto workDto = new WorkDto()
            {
                Id = work.WorkID,
                Title = work.Title,
                Copy = work.Copy,
                Description = work.Description,
                Artist = Convert(DaoFactory.GetArtistDao().Get(work.ArtistID))
            };
            return workDto;
        }
        public static Work Convert(WorkDto workDto)
        {
            if (workDto == null)
                return null;
            Work work = new Work()
            {
                WorkID = workDto.Id,
                Title = workDto.Title,
                Copy = workDto.Copy,
                Description = workDto.Description,
                ArtistID = workDto.Artist.Id
            };
            return work;
        }
        internal static IList<WorkDto> Convert(IList<Work> works)
        {
            IList<WorkDto> workDtos = new List<WorkDto>();
            foreach(Work work in works)
            {
                workDtos.Add(Convert(work));
            }
            return workDtos;
        }
        public static TransactionDto Convert(Transaction transaction)
        {
            if (transaction == null)
                return null;
            TransactionDto transactionDto = new TransactionDto()
            {
                Id = transaction.TransID,
                Work = ProcessFactory.GetWorkProcess().Get(transaction.WorkID),
                AcquisitionPrice = transaction.AcquisitionPrice,
                AskingPrice = transaction.AskingPrice,
                SalesPrice = transaction.SalesPrice,
                DateAcquired = transaction.DateAcquired,
                PurchaseDate = transaction.PurchaseDate
            };
            if (transaction.CustomerID != null)
                transactionDto.Customer = ProcessFactory.GetCustomerProcess().Get((int)transaction.CustomerID);
            else
                transactionDto.Customer = null;
            return transactionDto;
        }
        public static Transaction Convert(TransactionDto transactionDto)
        {
            if (transactionDto == null)
                return null;
            Transaction transaction = new Transaction()
            {
                WorkID = transactionDto.Work.Id,
                TransID = transactionDto.Id,
                DateAcquired = transactionDto.DateAcquired,
                PurchaseDate = transactionDto.PurchaseDate,
                AcquisitionPrice = transactionDto.AcquisitionPrice,
                AskingPrice = transactionDto.AskingPrice,
                SalesPrice = transactionDto.SalesPrice
            };
            if(transactionDto.Customer != null)
            {
                transaction.CustomerID = transactionDto.Customer.Id;
            }
            else
            {
                transaction.CustomerID = null;
            }
            return transaction;
        }
        internal static IList<TransactionDto> Convert(IList<Transaction> transactions)
        {
            IList<TransactionDto> transactionDtos = new List<TransactionDto>();
            foreach(Transaction trans in transactions)
            {
                transactionDtos.Add(Convert(trans));
            }
            return transactionDtos;
        }
        public static WorkInGalleryDto Convert(WorkInGallery work)
        {
            if (work == null)
                return null;
            WorkInGalleryDto workDto = new WorkInGalleryDto()
            {
                WorkID = work.WorkID,
                AskingPrice = work.AskingPrice,
                Copy = work.Copy,
                Description = work.Description,
                Name = work.Name,
                Title = work.Title
            };
            return workDto;
        }
        public static WorkInGallery Convert(WorkInGalleryDto workDto)
        {
            if (workDto == null)
                return null;
            WorkInGallery work = new WorkInGallery()
            {
                WorkID = workDto.WorkID,
                AskingPrice = workDto.AskingPrice,
                Copy = workDto.Copy,
                Description = workDto.Description,
                Name = workDto.Name,
                Title = workDto.Title
            };
            return work;
        }
        internal static IList<WorkInGalleryDto> Convert(IList<WorkInGallery> works)
        {
            IList<WorkInGalleryDto> workDtos = new List<WorkInGalleryDto>();
            foreach (WorkInGallery work in works)
            {
                workDtos.Add(Convert(work));
            }
            return workDtos;
        }

        public static ReportItemDto Convert(Report report)
        {
            if (report == null)
                return null;
            ReportItemDto reportDto = new ReportItemDto()
            {
                Date = report.Date.ToString(),
                Count = report.Count,
                Price = report.Price
            };
            return reportDto;
        }
        public static Report Convert(ReportItemDto reportDto)
        {
            if (reportDto == null)
                return null;
            Report report = new Report()
            {
                Date = DateTime.Parse(reportDto.Date),
                Count = reportDto.Count,
                Price = reportDto.Price
            };
            return report;
        }
        internal static IList<ReportItemDto> Convert(IList<Report> reports)
        {
            IList<ReportItemDto> reportDtos = new List<ReportItemDto>();
            foreach (Report report in reports)
            {
                reportDtos.Add(Convert(report));
            }
            return reportDtos;
        }
    }
}
