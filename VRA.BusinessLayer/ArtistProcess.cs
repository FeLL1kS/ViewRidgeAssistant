using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VRA.Dto;

namespace VRA.BusinessLayer
{
    class ArtistProcess : IArtistProcess
    {
        private static readonly IDictionary<int, ArtistDto> Artists = new Dictionary<int, ArtistDto>();
        public void Add(ArtistDto artist)
        {
            int max = Artists.Keys.Count == 0 ? 1 : Artists.Keys.Max() + 1; 
            artist.Id = max; 
            Artists.Add(max, artist);
        }

        public void Delete(int id)
        {
            if (Artists.ContainsKey(id)) 
                Artists.Remove(id);
        }

        public ArtistDto Get(int id)
        {
            if (Artists.ContainsKey(id))
                return Artists[id];
            return null;
        }

        public IList<ArtistDto> GetList()
        {
            return new List<ArtistDto>(Artists.Values);
        }

        public void Update(ArtistDto artist)
        {
            if (Artists.ContainsKey(artist.Id)) 
                Artists[artist.Id] = artist;
        }
    }
}
