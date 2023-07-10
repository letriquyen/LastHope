using Microsoft.EntityFrameworkCore;
using Repository.Enum;
using Repository.Models;
using Repository.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Repository.Repository.Implement
{
    public class RentContractRepository : IRentContractRepository
    {
        private readonly LastHopeDatabaseContext _context = new();

        public RentContractRepository()
        {
        }
        public RentContract? Add(RentContract rentContract)
        {
            var contract = _context.RentContracts.Add(rentContract);
            if (_context.SaveChanges() > 0)
            {
                return contract.Entity;
            }
            else return null;
        }

        public List<RentContract> Get()
        {
            return _context.RentContracts
                .OrderByDescending(c => c.StartDate)
                .Include(c => c.Flat.Building)
                .Include(c => c.Customer)
                .ToList();
        }

        public List<RentContract> GetAll()
        {
            return _context.RentContracts.ToList();
        }

        public RentContract? Get(int id)
        {
            return _context.RentContracts
                .Include(c => c.Flat.Building)
                .Include(c => c.Customer)
                .Include(c => c.Terms)
                .FirstOrDefault(rc => rc.Id == id);
        }

        public List<RentContract> Get(int recordPerPage, int pageNumber, out int totalPage)
        {
            totalPage = (int)Math.Ceiling(1.0 * _context.RentContracts.Count() / recordPerPage);
            return _context.RentContracts
                .OrderByDescending(c => c.StartDate)
                .Skip(recordPerPage * (pageNumber - 1))
                .Take(recordPerPage)
                .Include(c => c.Flat.Building)
                .Include(c => c.Customer)
                .ToList();
        }

        public List<RentContract> Get(int customerId, int recordPerPage, int pageNumber, out int totalPage)
        {
            totalPage = (int)Math.Ceiling(1.0 * _context.RentContracts.Where(c => c.CustomerId == customerId).Count() / recordPerPage);
            return _context.RentContracts
                .Where(c => c.CustomerId == customerId)
                .OrderByDescending(c => c.StartDate)
                .Skip(recordPerPage * (pageNumber - 1))
                .Take(recordPerPage)
                .Include(c => c.Flat.Building)
                .Include(c => c.Customer)
                .ToList();
        }

        public List<RentContract> Search(string name, int recordPerPage, int pageNumber, out int totalPage)
        {
            totalPage = (int)Math.Ceiling(1.0 * _context.RentContracts.Count() / recordPerPage);
            return _context.RentContracts
                .Where(c => c.Customer.Fullname.Contains(name))
                .OrderByDescending(c => c.StartDate)
                .Skip(recordPerPage * (pageNumber - 1))
                .Take(recordPerPage)
                .Include(c => c.Flat.Building)
                .Include(c => c.Customer)
                .ToList();
        }
        public bool Update(RentContract rentContract)
        {
            _context.RentContracts.Update(rentContract);
            return _context.SaveChanges() > 0;
        }

        public List<RentContract> GetAllValidContract()
        {
            return _context.RentContracts.Where(x=>x.Status.Equals(RentContractStatus.VALID))
                .Include(x => x.Flat.Building)
                .Include(x => x.Customer)
                .ToList();
        }

        
    }
}
