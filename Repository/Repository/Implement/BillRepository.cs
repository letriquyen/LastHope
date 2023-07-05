﻿using Microsoft.EntityFrameworkCore.ChangeTracking;
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
            return _context.Bills.ToList();
        }

        public bool Update(Bill bill)
        {
            bill.Status = 1;
            _context.Bills.Update(bill);
            return _context.SaveChanges() > 0;
        }

        public bool Add(Bill Bill)
        {
            _context.Bills.Add(Bill);
            return _context.SaveChanges() > 0;
        }

        public List<Bill> GetNewBillList()
        {
            return _context.Bills.Where(b => b.Status == 0).ToList();
        }

        public bool UpdateStatus(Bill bill)
        {
            bill.Status = 1;
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
