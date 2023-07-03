using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.Models;
using Repository.Repository.Interface;

namespace LastHope.Pages.Staff.RentContractPages
{
    public class CreateModel : PageModel
    {
        private readonly IRentContractRepository _rentContractRepository;
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IFlatRepository _flatRepository;
        private readonly IBuildingRepository _buildingRepository;

        public CreateModel(IRentContractRepository rentContractRepository, IUserAccountRepository userAccountRepository, 
            IFlatRepository flatRepository, IBuildingRepository buildingRepository)
        {
            _rentContractRepository = rentContractRepository;
            _userAccountRepository = userAccountRepository;
            _flatRepository = flatRepository;
            _buildingRepository = buildingRepository;
        }

        public IActionResult OnGet()
        {
            ViewData["CustomerId"] = new SelectList(_userAccountRepository.Get(), "Id", "Fullname");
            ViewData["BuildingId"] = new SelectList(_buildingRepository.Get(), "Id", "Name");
            return Page();
        }

        [BindProperty]
        public RentContract RentContract { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid || _rentContractRepository.Get() == null || RentContract == null)
            {
                return Page();
            }

            _rentContractRepository.Add(RentContract);


            return RedirectToPage("./Index");
        }
    }
}
