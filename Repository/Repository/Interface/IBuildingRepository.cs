using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Interface
{
    public interface IBuildingRepository
    {
        bool Add(Building building);
        bool Update(Building building);
        Building? Get(int id);
        List<Building> Get();
    }
}
