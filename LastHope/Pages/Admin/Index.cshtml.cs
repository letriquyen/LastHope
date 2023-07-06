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
    public class IndexModel : PageModel
    {
        private readonly IUserAccountRepository _userAccountRepository = new UserAccountRepository();

        public IList<UserAccount> UserAccount { get;set; } = default!;

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("Id") == null || HttpContext.Session.GetInt32("Role") != (int)Role.ADMIN)
            {
                return Redirect("/");
            }
            if (_userAccountRepository.Get() != null)
            {
                UserAccount = _userAccountRepository.Get();
            }
            return Page();
        }
    }
}
