using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VRA.Dto;
using VRA.DataAccess.Entities;

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
            artistDto.Nationality = artist.Nationality;
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
            artist.Nationality = artistDto.Nationality;
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
    }
}
