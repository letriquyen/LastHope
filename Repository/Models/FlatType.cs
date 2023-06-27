using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class FlatType
    {
        public FlatType()
        {
            Flats = new HashSet<Flat>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Flat> Flats { get; set; }
    }
}
