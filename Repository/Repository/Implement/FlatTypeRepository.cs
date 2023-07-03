using Repository.Models;
using Repository.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Implement
{
    public class FlatTypeRepository : IFlatTypeRepository
    {
        private readonly LastHopeDatabaseContext _context = new();
        public FlatTypeRepository()
        {

        }
        public List<FlatType> Get()
        {
            return _context.FlatTypes.ToList();
        }
    }
}
