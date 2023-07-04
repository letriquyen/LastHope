using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Interface
{
    public interface IBillRepository
    {
        bool Add(Bill bill);

        List<Bill> Get();

        List<Bill> GetNewBillList();

        Bill Get(int id);
    }
}
