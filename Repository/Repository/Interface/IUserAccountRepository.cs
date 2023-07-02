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
        public UserAccount Login(string phone, string password);
    }
}
