using Microsoft.EntityFrameworkCore;
using Repository.Models;
using Repository.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            _context.RentContracts.Add(rentContract);
            if (_context.SaveChanges() > 0)
            {
                return rentContract;
            }
            else return null;
        }

        public List<RentContract> Get()
        {
            return _context.RentContracts
                .Include(c => c.Flat.Building)
                .Include(c => c.Customer)
                .ToList();
        }

        public RentContract? Get(int id)
        {
            return _context.RentContracts
                .Include(c => c.Flat.Building)
                .Include(c => c.Customer)
                .Include(c => c.Terms)
                .FirstOrDefault(rc => rc.Id == id);
        }

        public bool Update(RentContract rentContract)
        {
            _context.RentContracts.Update(rentContract);
            return _context.SaveChanges() > 0;
        }
    }
}
