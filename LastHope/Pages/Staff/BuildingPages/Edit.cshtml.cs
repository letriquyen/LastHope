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

namespace LastHope.Pages.Staff.BuildingPages
{
    public class EditModel : PageModel
    {
        private readonly IBuildingRepository _buildingRepository;

        public EditModel(IBuildingRepository buildingRepository)
        {
            _buildingRepository = buildingRepository;
        }

        [BindProperty]
        public Building Building { get; set; } = default!;

        public IActionResult OnGet(int? id)
        {
            if (id == null || _buildingRepository.Get() == null)
            {
                return NotFound();
            }

            var building = _buildingRepository.Get(id.Value);
            if (building == null)
            {
                return NotFound();
            }
            Building = building;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }



            try
            {
                if (!_buildingRepository.Update(Building))
                {
                    ViewData["Error"] = "Update building fail";
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuildingExists(Building.Id))
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

        private bool BuildingExists(int id)
        {
          return _buildingRepository.Get(id) != null;
        }
    }
}
