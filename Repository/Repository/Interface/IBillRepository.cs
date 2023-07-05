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
        bool Add(Bill bill, List<BillItem> billItems);
        bool Update(Bill bill);
        Bill? Get(int id);
        List<Bill> Get();
    }
}
