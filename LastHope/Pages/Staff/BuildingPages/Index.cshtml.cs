using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using Repository.Repository.Interface;

namespace LastHope.Pages.Staff.BuildingPages
{
    public class IndexModel : PageModel
    {
        private readonly IBuildingRepository _buildingRepository;

        public IndexModel(IBuildingRepository buildingRepository)
        {
            _buildingRepository = buildingRepository;
        }

        public IList<Building> Building { get;set; } = default!;

        public void OnGet()
        {
            Building = _buildingRepository.Get();
        }
    }
}
