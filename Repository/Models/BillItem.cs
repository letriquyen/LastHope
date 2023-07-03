using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class BillItem
    {
        public int? BillId { get; set; }
        public int? ServiceId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Value { get; set; }

        public virtual Bill? Bill { get; set; }
        public virtual Service? Service { get; set; }
    }
}
