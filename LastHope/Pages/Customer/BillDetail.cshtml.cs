using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using Repository.Repository.Interface;

namespace LastHope.Pages.Customer
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

        public IActionResult OnPostPayBill ()
        {
            var id = HttpContext.Session.GetInt32("id");
            NewBill = _billRepository.Get((int)id);
            _billRepository.UpdateStatus(NewBill);
            return RedirectToPage("/Customer/ListBill");
        }
    }
}
