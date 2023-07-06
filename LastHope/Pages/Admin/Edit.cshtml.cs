using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repository.Enum;
using Repository.Models;
using Repository.Repository.Implement;
using Repository.Repository.Interface;

namespace LastHope.Pages.Admin
{
    public class EditModel : PageModel
    {
        private readonly IUserAccountRepository _userAccountRepository = new UserAccountRepository();

        [BindProperty]
        public UserAccount UserAccount { get; set; } = default!;

        public IActionResult OnGet(int id)
        {
            if (HttpContext.Session.GetString("Id") == null || HttpContext.Session.GetInt32("Role") != (int)Role.ADMIN)
            {
                return Redirect("/");
            }
            if (id == null || _userAccountRepository.Get() == null)
            {
                return NotFound();
            }

            var useraccount =  _userAccountRepository.Get(id);
            if (useraccount == null)
            {
                return NotFound();
            }
            UserAccount = useraccount;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                _userAccountRepository.Update(UserAccount);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_userAccountRepository.UserAccountExists(UserAccount.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

    }
}
