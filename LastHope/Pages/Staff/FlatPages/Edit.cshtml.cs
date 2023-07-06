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

namespace LastHope.Pages.Staff.FlatPages
{
    public class EditModel : PageModel
    {
        private readonly IFlatRepository _flatRepository;
        private readonly IFlatTypeRepository _flatTypeRepository;
        private readonly IBuildingRepository _buildingRepository;

        public EditModel(IFlatRepository flatRepository, IFlatTypeRepository flatTypeRepository,
            IBuildingRepository buildingRepository)
        {
            _flatRepository = flatRepository;
            _flatTypeRepository = flatTypeRepository;
            _buildingRepository = buildingRepository;
        }

        [BindProperty]
        public Flat Flat { get; set; } = default!;

        public IActionResult OnGet(int? id)
        {
            if (HttpContext.Session.GetString("Id") == null || HttpContext.Session.GetInt32("Role") != (int)Role.STAFF)
            {
                return Redirect("/");
            }
            if (id == null || _flatRepository.Get() == null)
            {
                return NotFound();
            }

            var flat = _flatRepository.Get(id.Value);
            if (flat == null)
            {
                return NotFound();
            }
            Flat = flat;
            ViewData["BuildingId"] = new SelectList(_buildingRepository.Get(), "Id", "Name");
            ViewData["FlatTypeId"] = new SelectList(_flatTypeRepository.Get(), "Id", "Name");
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
                _flatRepository.Update(Flat);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FlatExists(Flat.Id))
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

        private bool FlatExists(int id)
        {
          return _flatRepository.Get(id) != null;
        }
    }
}
