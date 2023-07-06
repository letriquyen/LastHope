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

namespace LastHope.Pages.Staff.FlatPages
{
    public class CreateModel : PageModel
    {
        private readonly IFlatRepository _flatRepository;
        private readonly IFlatTypeRepository _flatTypeRepository;
        private readonly IBuildingRepository _buildingRepository;

        public CreateModel(IFlatRepository flatRepository, IFlatTypeRepository flatTypeRepository, 
            IBuildingRepository buildingRepository)
        {
            _flatRepository = flatRepository;
            _flatTypeRepository = flatTypeRepository;
            _buildingRepository = buildingRepository;
        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("Id") == null || HttpContext.Session.GetInt32("Role") != (int)Role.STAFF)
            {
                return Redirect("/");
            }
            ViewData["BuildingId"] = new SelectList(_buildingRepository.Get(), "Id", "Name");
            ViewData["FlatTypeId"] = new SelectList(_flatTypeRepository.Get(), "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Flat Flat { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid || _flatRepository.Get() == null || Flat == null)
            {
                return Page();
            }

            _flatRepository.Add(Flat);


            return RedirectToPage("./Index");
        }
    }
}
