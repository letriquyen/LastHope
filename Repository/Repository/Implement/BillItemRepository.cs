using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Implement
{
    public class BillItemRepository
    {
        private readonly LastHopeDatabaseContext _context = new();
        public BillItemRepository()
        {
        }
        public bool Add(BillItem billItem)
        {
            _context.BillItems.Add(billItem);
            return _context.SaveChanges() > 0;
        }

        public BillItem? Get(int id)
        {
            return _context.BillItems.FirstOrDefault(b => b.BillId == id);
        }

        public List<BillItem> Get()
        {
            return _context.BillItems.ToList();
        }

        public bool Update(BillItem billItem)
        {
            _context.BillItems.Update(billItem);
            return _context.SaveChanges() > 0;
        }
    }
}
