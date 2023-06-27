using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository.Models;

namespace LastHope.Pages.UserAccounts
{
    public class IndexModel : PageModel
    {
        private readonly Repository.Models.LastHopeDatabaseContext _context;

        public IndexModel(Repository.Models.LastHopeDatabaseContext context)
        {
            _context = context;
        }

        public IList<UserAccount> UserAccount { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.UserAccounts != null)
            {
                UserAccount = await _context.UserAccounts.ToListAsync();
            }
        }
    }
}
