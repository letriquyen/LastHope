using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using Repository.Repository.Interface;

namespace LastHope.Pages.Staff.RentContractPages
{
    public class DetailsModel : PageModel
    {
        private readonly IRentContractRepository _rentContractRepository;

        public DetailsModel(IRentContractRepository rentContractRepository)
        {
            _rentContractRepository = rentContractRepository;
        }

        public RentContract RentContract { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
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
    }
}
