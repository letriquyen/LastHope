using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class Term
    {
        public int Id { get; set; }
        public int? RentContractId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }

        public virtual RentContract? RentContract { get; set; }
    }
}
