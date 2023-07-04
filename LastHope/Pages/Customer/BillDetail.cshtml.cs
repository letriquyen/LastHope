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

        public IActionResult OnGet(int id)
        {
            if (id == null || _billRepository.Get() == null)
            {
                return NotFound();
            }

            Bill = _billRepository.Get(id);

            if (Bill == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
