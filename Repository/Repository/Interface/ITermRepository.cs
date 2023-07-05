using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Interface
{
    public interface ITermRepository
    {
        bool Add(Term term);
        bool Update(List<Term> terms);
    }
}
