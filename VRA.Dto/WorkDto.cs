using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VRA.Dto
{
    public class WorkDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Copy { get; set; }
        public string Description { get; set; }
        public ArtistDto Artist { get; set; }
    }
}
