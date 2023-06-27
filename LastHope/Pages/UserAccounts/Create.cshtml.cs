using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.Models;

namespace LastHope.Pages.UserAccounts
{
    public class CreateModel : PageModel
    {
        private readonly Repository.Models.LastHopeDatabaseContext _context;

        public CreateModel(Repository.Models.LastHopeDatabaseContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public UserAccount UserAccount { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.UserAccounts == null || UserAccount == null)
            {
                return Page();
            }

            _context.UserAccounts.Add(UserAccount);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
