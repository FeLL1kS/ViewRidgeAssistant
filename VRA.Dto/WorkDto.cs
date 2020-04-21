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
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            WorkDto workItem = obj as WorkDto;
            return workItem != null && workItem.Id.Equals(this.Id);
        }
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
