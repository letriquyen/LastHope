using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.Models;
using Repository.Repository.Implement;
using Repository.Repository.Interface;

namespace LastHope.Pages.Staff.BuildingPages
{
    public class CreateModel : PageModel
    {
        private readonly IBuildingRepository _buildingRepository;

        public CreateModel(IBuildingRepository buildingRepository)
        {
            _buildingRepository = buildingRepository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Building Building { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid || _buildingRepository.Get() == null || Building == null)
            {
                return Page();
            }

            _buildingRepository.Add(Building);

            return RedirectToPage("./Index");
        }
    }
}
