using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Repository.Enum;
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

        public Bill? Get(int id)
        {
            return _context.Bills.FirstOrDefault(b => b.Id == id);
        }

        public List<Bill> Get()
        {
            return _context.Bills
                .Include(b => b.RentContract.Flat.Building)
                .OrderBy(b => b.Status)
                .OrderByDescending(b => b.Date)
                .ToList();
        }
        public List<Bill> Get(string customerName, int pageNumber, int recordPerPage, out int totalPage)
        {
            totalPage = (int)Math.Ceiling(1.0 * _context.Bills.Where(b => b.RentContract.Customer.Fullname.Contains(customerName)).Count() / recordPerPage);
            return _context.Bills
                .Include(b => b.RentContract.Customer)
                .Include(b => b.RentContract.Flat.Building)
                .Where(b => b.RentContract.Customer.Fullname.Contains(customerName))
                .Skip((pageNumber - 1) * recordPerPage)
                .Take(recordPerPage)
                .OrderBy(b => b.Status)
                .OrderByDescending(b => b.Date)
                .ToList();
        }
        public bool Update(Bill bill)
        {
            bill.Status = BillStatus.PAID;
            _context.Bills.Update(bill);
            return _context.SaveChanges() > 0;
        }

        public bool Add(Bill Bill)
        {
            _context.Bills.Add(Bill);
            return _context.SaveChanges() > 0;
        }

        public List<Bill> GetNewBillList(int id)
        {
            return _context.Bills.Where(b => b.Status == 0 && b.RentContract.CustomerId == id).ToList();
        }

        public List<Bill> GetBillByCustomerID(int id)
        {
            return _context.Bills.Where(b => b.RentContract.CustomerId == id).ToList();
        }

        public bool UpdateStatus(Bill bill)
        {
            bill.Status = BillStatus.PAID;
            _context.Bills.Update(bill);
            return _context.SaveChanges() > 0;
        }

        public Bill AddBill(Bill bill)
        {
            EntityEntry<Bill> billEntry = _context.Bills.Add(bill);
            _context.SaveChanges();
            bill = billEntry.Entity;

            return bill;
        }
    }
}
