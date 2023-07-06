using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Interface
{
    public interface IUserAccountRepository
    {
        UserAccount Login(string phone, string password);
        List<UserAccount> Get();
        UserAccount Get(int id);
        bool UserAccountExists(int id);
        bool Update(UserAccount account);
        bool Delete(UserAccount account);
        bool Create(UserAccount account);
    }
}
