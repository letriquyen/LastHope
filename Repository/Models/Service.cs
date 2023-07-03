using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class Service
    {
        public Service()
        {
            BillItems = new HashSet<BillItem>();
        }

        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int? Status { get; set; }

        public virtual ICollection<BillItem> BillItems { get; set; }
    }
}
