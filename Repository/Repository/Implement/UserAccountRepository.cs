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

        public bool Create(UserAccount account)
        {
            _context.UserAccounts.Add(account);
            return _context.SaveChanges() > 0;
        }

        public bool Delete(UserAccount account)
        {
            _context.UserAccounts.Remove(account);
            return _context.SaveChanges() > 0;
        }

        public List<UserAccount> Get()
        {
            return _context.UserAccounts.ToList();
        }

        public UserAccount Get(int id)
        {
            return _context.UserAccounts.FirstOrDefault(a => a.Id == id);
        }

        public UserAccount Login(string phone, string password)
        {
            return _context.UserAccounts.FirstOrDefault(a => a.Phone.Equals(phone) && a.Password.Equals(password));
        }

        public bool Update(UserAccount account)
        {
            _context.UserAccounts.Update(account);
            return _context.SaveChanges() > 0;
        }

        public bool UserAccountExists(int id)
        {
            return (_context.UserAccounts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
