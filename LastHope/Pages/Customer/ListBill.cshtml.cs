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

namespace LastHope.Pages.Customer
{
    public class ListBillModel : PageModel
    {
        private readonly IBillRepository _billRepository;

        public ListBillModel(IBillRepository billRepository)
        {
            _billRepository = billRepository;
        }

        public List<Bill> Bill { get;set; } = default!;

 

        public List<Bill> NewBill { get; set; } = default!;

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("Id") == null || HttpContext.Session.GetInt32("Role") != (int)Role.CUSTOMER)
            {
                return Redirect("/");
            }
            Bill = _billRepository.Get();
            NewBill = _billRepository.GetNewBillList();
            return Page();
        }
    }
}
