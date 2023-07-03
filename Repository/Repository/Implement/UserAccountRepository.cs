using Repository.Models;
using Repository.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Implement
{
    public class UserAccountRepository : IUserAccountRepository
    {
        private LastHopeDatabaseContext _context = new();
        public UserAccountRepository() { }

        public List<UserAccount> Get()
        {
            return _context.UserAccounts.ToList();
        }

        public UserAccount Login(string phone, string password)
        {
            return _context.UserAccounts.FirstOrDefault(a => a.Phone.Equals(phone) && a.Password.Equals(password));
        }
    }
}
