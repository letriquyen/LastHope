using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.Enum;
using Repository.Models;
using Repository.Repository.Interface;

namespace LastHope.Pages.Staff.MonthlyBill
{
    public class CreateModel : PageModel
    {
        private readonly IBillRepository _billRepository;
        private readonly IRentContractRepository _rentContractRepository;
        public CreateModel(IBillRepository billRepository, IRentContractRepository rentContractRepository)
        {
            _billRepository = billRepository;
            _rentContractRepository = rentContractRepository;
        }
        List<int> statusList = new List<int> { 0, 1 };

        [BindProperty]
        public Bill Bill { get; set; } = default!;

        public IActionResult OnGet(int id)
        {
            if (HttpContext.Session.GetString("Id") == null || HttpContext.Session.GetInt32("Role") != (int)Role.STAFF)
            {
                return Redirect("/");
            }
            Bill.RentContractId = id;
            //ViewData["StatusList"] = new SelectList(statusList, "Id", "Id");
            ViewData["StatusList"] = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Value = "0", Text = "Not paid" },
                new SelectListItem { Value = "1", Text = "Paid" }
            }, "Value", "Text", 0);
            return Page();
        }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid || _billRepository.Get() == null || Bill == null)
            {
                return Page();
            }

            _billRepository.Add(Bill);

            return RedirectToPage("/Staff/MonthlyBill/Create");
        }
    }
}
