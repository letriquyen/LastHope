using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Interface
{
    public interface IFlatRepository
    {
        bool Add(Flat flat);
        bool Update(Flat flat);
        Flat? Get(int id);
        List<Flat> Get();
        List<Flat> Get(int recordPerPage, int pageNumber, out int totalPage);
        List<Flat> GetByBuilding(int buildingId);
    }
}
