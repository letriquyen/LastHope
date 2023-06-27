using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class Bill
    {
        public Bill()
        {
            BillItems = new HashSet<BillItem>();
        }

        public int Id { get; set; }
        public int? RentContractId { get; set; }
        public DateTime? Date { get; set; }
        public decimal? Value { get; set; }
        public int? Status { get; set; }
        public string? Sender { get; set; }
        public string? Receiver { get; set; }
        public string? Content { get; set; }

        public virtual RentContract? RentContract { get; set; }
        public virtual ICollection<BillItem> BillItems { get; set; }
    }
}
