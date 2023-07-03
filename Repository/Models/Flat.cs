using Repository.Enum;
using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class Flat
    {
        public int Id { get; set; }
        public string? Detail { get; set; }
        public decimal? Price { get; set; }
        public FlatStatus? Status { get; set; }
        public int? BuildingId { get; set; }
        public int? FlatTypeId { get; set; }
        public int? RoomNumber { get; set; }

        public virtual Building? Building { get; set; }
        public virtual FlatType? FlatType { get; set; }
    }
}
