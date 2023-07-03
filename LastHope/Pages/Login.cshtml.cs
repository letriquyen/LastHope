using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Repository.Implement;
using Repository.Repository.Interface;

namespace LastHope.Pages
{
    public class LoginModel : PageModel
    {
        public IUserAccountRepository _repository = new UserAccountRepository();
        [BindProperty]
        public string Phone { get; set; }
        public string Password { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            var account = _repository.Login(Phone, Password);
            if (account != null)
            {
                HttpContext.Session.SetInt32("Role", account.RoleUser);
                HttpContext.Session.SetInt32("Id", account.Id);
                switch (account.RoleUser)
                {
                    case 0:
                        return RedirectToPage("Admin/Index"); // tuong trung
                        
                    case 1:
                        return RedirectToPage("Staff/Building/Index");
                        
                    case 2:
                        return RedirectToPage("Customer/Index");
                        
                }

            }
            ViewData["Message"] = "Wrong phone or password!";
            return Page();
        }
    }
}
