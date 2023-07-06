using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.Enum;
using Repository.Models;
using Repository.Repository.Implement;
using Repository.Repository.Interface;

namespace LastHope.Pages.Admin
{
    public class CreateModel : PageModel
    {
        private readonly IUserAccountRepository _userAccountRepository = new UserAccountRepository();

        public IActionResult OnGet()
        {
            var roles = Enum.GetValues(typeof(Role)).Cast<Role>().ToList();
            ViewData["Role"] = new SelectList(roles.Select((value, index) => new { value, index }), "index", "value");
            return Page();
        }

        [BindProperty]
        public UserAccount UserAccount { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _userAccountRepository.Get() == null || UserAccount == null)
            {
                var roles = Enum.GetValues(typeof(Role)).Cast<Role>().ToList();
                ViewData["Roles"] = new SelectList(roles.Select((value, index) => new { value, index }), "index", "value");
                return Page();
            }
            _userAccountRepository.Create(UserAccount);

            return RedirectToPage("./Index");
        }
    }
}
