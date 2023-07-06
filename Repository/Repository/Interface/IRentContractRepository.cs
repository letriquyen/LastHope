using Microsoft.EntityFrameworkCore;
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

        List<RentContract> GetAll();
        List<RentContract> Get(int recordPerPage, int pageNumber, out int totalPage);
        List<RentContract> Search(string name, int recordPerPage, int pageNumber, out int totalPage);
        RentContract? Get(int id);
    }
}
