using Repository.Models;
using Repository.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Implement
{
    public class BillRepository : IBillRepository
    {
        private readonly LastHopeDatabaseContext _context = new();

        public BillRepository()
        {
        }

        public bool Add(Bill Bill)
        {
            _context.Bills.Add(Bill);
            return _context.SaveChanges() > 0;
        }

        public List<Bill> Get()
        {
            return _context.Bills.ToList();
        }

        public List<Bill> GetNewBillList()
        {
            return _context.Bills.Where(b => b.Status == 0).ToList();
        }

        public Bill Get(int id)
        {
            return _context.Bills.FirstOrDefault(b => b.Id == id);
        }

        public bool UpdateStatus(Bill bill)
        {
            bill.Status = 1;
            _context.Bills.Update(bill);
            return _context.SaveChanges() > 0;
        }
    }
}
