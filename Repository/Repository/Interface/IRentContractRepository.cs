using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Interface
{
    public interface IRentContractRepository
    {
        RentContract? Add(RentContract rentContract);
        bool Update(RentContract rentContract); 
        List<RentContract> Get();
        RentContract? Get(int id);
    }
}
