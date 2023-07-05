using Repository.Models;
using Repository.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Implement
{
    public class TermRepository : ITermRepository
    {
        private readonly LastHopeDatabaseContext _context = new();

        public TermRepository()
        {
        }

        public bool Add(Term term)
        {
            _context.Terms.Add(term);
            return _context.SaveChanges() > 0;
        }

        public bool Update(List<Term> terms)
        {
            _context.Terms.UpdateRange(terms);
            return _context.SaveChanges() > 0;
        }
    }
}
