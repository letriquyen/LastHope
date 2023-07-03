using Repository.Models;
using Repository.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Implement
{
    public class BuildingRepository : IBuildingRepository
    {
        private readonly LastHopeDatabaseContext _context = new();
        public BuildingRepository()
        {
        }
        public bool Add(Building building)
        {
            _context.Buildings.Add(building);
            return _context.SaveChanges() > 0;
        }

        public Building? Get(int id)
        {
            return _context.Buildings.FirstOrDefault(b => b.Id == id);
        }

        public List<Building> Get()
        {
            return _context.Buildings.ToList();
        }

        public bool Update(Building building)
        {
            _context.Buildings.Update(building);
            return _context.SaveChanges() > 0;
        }
    }
}
