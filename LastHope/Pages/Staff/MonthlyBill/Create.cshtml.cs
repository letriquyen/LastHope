using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public IActionResult OnGet()
        {
            ViewData["RentContractId"] = new SelectList(_rentContractRepository.Get(), "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Bill Bill { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid || _billRepository.Get() == null || Bill == null)
            {
                return Page();
            }

            _billRepository.Add(Bill);

            return RedirectToPage("/Privacy");
        }
    }
}
