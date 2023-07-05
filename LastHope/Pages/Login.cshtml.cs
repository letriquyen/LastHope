using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Enum;
using Repository.Repository.Implement;
using Repository.Repository.Interface;

namespace LastHope.Pages
{
    public class LoginModel : PageModel
    {
        public IUserAccountRepository _repository = new UserAccountRepository();
        [BindProperty]
        public string Phone { get; set; }
        [BindProperty]
        public string Password { get; set; }
        public IActionResult OnGet()
        {
            HttpContext.Session.Clear();
            return Page();
        }
        public IActionResult OnPost()
        {
            var account = _repository.Login(Phone, Password);
            if (account != null)
            {
                HttpContext.Session.SetInt32("Role", (int)account.RoleUser);
                HttpContext.Session.SetInt32("Id", account.Id);
                switch (account.RoleUser)
                {
                    case Role.ADMIN:
                        return RedirectToPage("Admin/Index"); // tuong trung
                        
                    case Role.STAFF:
                        return RedirectToPage("/Staff/BuildingPages/Index");
                        
                    case Role.CUSTOMER:
                        return RedirectToPage("Customer/Index");
                        
                }

            }
            ViewData["Message"] = "Wrong phone or password!";
            return Page();
        }

        
    }
}
