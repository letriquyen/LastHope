using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository.Enum;
using Repository.Models;
using Repository.Repository.Implement;
using Repository.Repository.Interface;

namespace LastHope.Pages.Admin
{
    public class DetailsModel : PageModel
    {
        private readonly IUserAccountRepository _userAccountRepository = new UserAccountRepository();

      public UserAccount UserAccount { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (HttpContext.Session.GetString("Id") == null || HttpContext.Session.GetInt32("Role") != (int)Role.ADMIN)
            {
                return Redirect("/");
            }
            if (id == null || _userAccountRepository.Get() == null)
            {
                return NotFound();
            }

            var useraccount = _userAccountRepository.Get(id);
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
