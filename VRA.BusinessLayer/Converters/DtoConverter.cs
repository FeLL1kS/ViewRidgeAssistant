﻿using System;
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
    }
}
