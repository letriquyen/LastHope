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
    public class DetailsModel : PageModel
    {
        private readonly Repository.Models.LastHopeDatabaseContext _context;

        public DetailsModel(Repository.Models.LastHopeDatabaseContext context)
        {
            _context = context;
        }

      public UserAccount UserAccount { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.UserAccounts == null)
            {
                return NotFound();
            }

            var useraccount = await _context.UserAccounts.FirstOrDefaultAsync(m => m.Id == id);
            if (useraccount == null)
            {
                return NotFound();
            }
            else 
            {
                UserAccount = useraccount;
            }
            return Page();
        }
    }
}
