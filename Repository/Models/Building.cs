﻿using Repository.Enum;
using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class Building
    {
        public Building()
        {
            Flats = new HashSet<Flat>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public BuildingStatus? Status { get; set; }
        public int? Capacity { get; set; }

        public virtual ICollection<Flat> Flats { get; set; }
        
    }
}
