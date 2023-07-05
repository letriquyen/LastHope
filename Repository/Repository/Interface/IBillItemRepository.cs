using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Interface
{
    public interface IBillItemRepository
    {
        bool Add(BillItem bill);
        bool Update(BillItem bill);
        BillItem? Get(int id);
        List<BillItem> Get();
    }
}
