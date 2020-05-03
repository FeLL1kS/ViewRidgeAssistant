using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VRA.Dto
{
    public class WorkInGalleryDto
    {
        public decimal? AskingPrice { get; set; }
        public string Name { get; set; }
        public int WorkID { get; set; }
        public string Title { get; set; }
        public string Copy { get; set; }
        public string Description { get; set; }
    }
}
