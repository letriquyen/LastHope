using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using Repository.Repository.Interface;

namespace LastHope.Pages.Staff.RentContractPages
{
    public class EditModel : PageModel
    {
        private readonly IRentContractRepository _rentContractRepository;
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IFlatRepository _flatRepository;
        private readonly IBuildingRepository _buildingRepository;

        public EditModel(IRentContractRepository rentContractRepository, IUserAccountRepository userAccountRepository,
            IFlatRepository flatRepository, IBuildingRepository buildingRepository)
        {
            _rentContractRepository = rentContractRepository;
            _userAccountRepository = userAccountRepository;
            _flatRepository = flatRepository;
            _buildingRepository = buildingRepository;
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
            RentContract = rentcontract;
            ViewData["CustomerId"] = new SelectList(_userAccountRepository.Get(), "Id", "Fullname");
            ViewData["BuildingId"] = new SelectList(_buildingRepository.Get(), "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            try
            {
                _rentContractRepository.Update(RentContract);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentContractExists(RentContract.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool RentContractExists(int id)
        {
            return _rentContractRepository.Get(id) != null;
        }
    }
}
