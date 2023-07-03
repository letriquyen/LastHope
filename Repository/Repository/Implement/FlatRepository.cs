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
    public class FlatRepository : IFlatRepository
    {
        private readonly LastHopeDatabaseContext _context = new();
        public FlatRepository()
        {

        }

        public bool Add(Flat flat)
        {
            _context.Flats.Add(flat);
            return _context.SaveChanges() > 0;
        }

        public Flat? Get(int id)
        {
            return _context.Flats
                .Include(f => f.Building)
                .Include(f => f.FlatType)
                .FirstOrDefault(flat => flat.Id == id);    
        }

        public List<Flat> Get()
        {
            return _context.Flats
                .Include(f => f.Building)
                .Include(f => f.FlatType)
                .ToList();
        }

        public List<Flat> GetByBuilding(int buildingId)
        {
            return _context.Flats
                .Include(f => f.Building)
                .Include(f => f.FlatType)
                .Where(f => f.BuildingId == buildingId)
                .ToList();
        }

        public bool Update(Flat flat)
        {
            _context.Flats.Update(flat);
            return _context.SaveChanges() > 0;
        }
    }
}
