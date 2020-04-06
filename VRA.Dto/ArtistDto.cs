using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VRA.Dto
{
    public class ArtistDto
    {
        // ID художника
        public int Id { get; set; }
        // Имя художник
        public string Name { get; set; }
        // Год рождения художника
        public int BirthYear { get; set; }
        // Год смерти художника
        public int? DeceaseYear { get; set; }
        // Национальность
        public string Nationality { get; set; }
    }
}
