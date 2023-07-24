using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository.Enum;
using Repository.Models;
using Repository.Repository.Interface;

namespace LastHope.Pages.Staff.BillPage
{
    public class BillDetailModel : PageModel
    {
        private readonly IBillRepository _billRepository;

        public BillDetailModel(IBillRepository billRepository)
        {
            _billRepository = billRepository;

        }

        public Bill Bill { get; set; } = default!; 

        private Bill NewBill { get; set; }
        
        public IActionResult OnGet(int id)
        {
            if (HttpContext.Session.GetString("Id") == null || HttpContext.Session.GetInt32("Role") != (int)Role.STAFF)
            {
                return Redirect("/");
            }
            if (id == null || _billRepository.Get() == null)
            {
                return NotFound();
            }
            
            HttpContext.Session.SetInt32("id", id);

            Bill = _billRepository.Get(id);
            

            if (Bill == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
