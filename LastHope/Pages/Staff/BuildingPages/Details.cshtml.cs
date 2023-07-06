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

namespace LastHope.Pages.Staff.BuildingPages
{
    public class DetailsModel : PageModel
    {
        private readonly IBuildingRepository _buildingRepository;

        public DetailsModel(IBuildingRepository buildingRepository)
        {
            _buildingRepository = buildingRepository;
        }

        public Building Building { get; set; } = default!;

        public IActionResult OnGet(int? id)
        {
            if (HttpContext.Session.GetString("Id") == null || HttpContext.Session.GetInt32("Role") != (int)Role.STAFF)
            {
                return Redirect("/");
            }
            if (id == null || _buildingRepository.Get() == null)
            {
                return NotFound();
            }

            var building = _buildingRepository.Get(id.Value);
            if (building == null)
            {
                return NotFound();
            }
            else
            {
                Building = building;
            }
            return Page();
        }
    }
}
