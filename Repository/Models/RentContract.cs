using Repository.Enum;
using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class RentContract
    {
        public RentContract()
        {
            Bills = new HashSet<Bill>();
            Terms = new HashSet<Term>();
        }

        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public int FlatId { get; set; }
        public decimal? Value { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public RentContractStatus? Status { get; set; }
        public string? Contract { get; set; }
        public string? Title { get; set; }

        public virtual UserAccount? Customer { get; set; }
        public virtual Flat? Flat { get; set; }
        public virtual ICollection<Bill> Bills { get; set; }
        public virtual ICollection<Term> Terms { get; set; }
    }
}
