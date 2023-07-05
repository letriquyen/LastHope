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
        bool Update(Bill bill);
        Bill? Get(int id);
        bool Add(Bill bill);
        Bill AddBill(Bill bill);
        List<Bill> Get();

        List<Bill> GetNewBillList();

        bool UpdateStatus(Bill bill);
    }
}
