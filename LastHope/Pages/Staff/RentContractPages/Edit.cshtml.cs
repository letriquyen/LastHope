using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repository.Enum;
using Repository.Models;
using Repository.Repository.Interface;

namespace LastHope.Pages.Staff.RentContractPages
{
    public class EditModel : PageModel
    {
        private readonly IRentContractRepository _rentContractRepository;
        private readonly IFlatRepository _flatRepository;
        private readonly IBuildingRepository _buildingRepository;
        private readonly ITermRepository _termRepository;
        private readonly IUserAccountRepository _userAccountRepository;

        public EditModel(IRentContractRepository rentContractRepository, IFlatRepository flatRepository,
            IBuildingRepository buildingRepository, ITermRepository termRepository,
            IUserAccountRepository userAccountRepository)
        {
            _rentContractRepository = rentContractRepository;
            _flatRepository = flatRepository;
            _buildingRepository = buildingRepository;
            _termRepository = termRepository;
            _userAccountRepository = userAccountRepository;
        }

        [BindProperty]
        public RentContract RentContract { get; set; } = default!;
        [BindProperty]
        public int? BuildingId { get; set; }
       
        [BindProperty]
        public List<Term> Terms { get; set; }
        public IActionResult OnGet(int? id, int? BuildingId)
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
            if (BuildingId == null)
            {
                this.BuildingId = RentContract.Flat.BuildingId;
            }
            else this.BuildingId = BuildingId;
            Terms = RentContract.Terms.ToList();
            LoadData(RentContract);
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
                _termRepository.Update(Terms);
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
        private void LoadData(RentContract contract)
        {
            ViewData["CustomerId"] = new SelectList(_userAccountRepository.Get(), "Id", "Fullname", contract.CustomerId);
            ViewData["BuildingId"] = new SelectList(_buildingRepository.Get(), "Id", "Name", contract.Flat.BuildingId);
            var flats = _flatRepository.GetByBuilding(BuildingId.Value);
            var statuses = Enum.GetValues(typeof(RentContractStatus)).Cast<RentContractStatus>().ToList();
            ViewData["Status"] = new SelectList(statuses.Select((value, index) => new { value, index }), "index", "value", contract.Status);
            ViewData["FlatId"] = new SelectList(flats, "Id", "RoomNumber", contract.Flat.RoomNumber);
        }

    
    }
}
