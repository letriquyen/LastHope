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

namespace LastHope.Pages.Staff.RentContractPages
{
    public class DeleteModel : PageModel
    {
        private readonly IRentContractRepository _rentContractRepository;

        public DeleteModel(IRentContractRepository rentContractRepository)
        {
            _rentContractRepository = rentContractRepository;
        }

        [BindProperty]
      public RentContract RentContract { get; set; } = default!;

        public IActionResult OnGet(int? id)
        {
            if (id == null || _rentContractRepository.Get() == null)
            {
                return NotFound();
            }

            var rentcontract = _rentContractRepository.Get(id.Value);

            if (rentcontract == null)
            {
                return NotFound();
            }
            else
            {
                RentContract = rentcontract;
            }
            return Page();
        }

        public IActionResult OnPost(int? id)
        {
            if (id == null || _rentContractRepository.Get() == null)
            {
                return NotFound();
            }
            var rentcontract = _rentContractRepository.Get(id.Value);

            if (rentcontract != null)
            {
                rentcontract.Status = RentContractStatus.DELETED;
                RentContract = rentcontract;
                _rentContractRepository.Update(RentContract);

            }

            return RedirectToPage("./Index");
        }
    }
}
