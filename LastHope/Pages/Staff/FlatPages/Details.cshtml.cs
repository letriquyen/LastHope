using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using Repository.Repository.Interface;

namespace LastHope.Pages.Staff.FlatPages
{
    public class DetailsModel : PageModel
    {
        private readonly IFlatRepository _flatRepository;

        public DetailsModel(IFlatRepository flatRepository)
        {
            _flatRepository = flatRepository;
        }

        public Flat Flat { get; set; } = default!;

        public IActionResult OnGet(int? id)
        {
            if (id == null || _flatRepository.Get() == null)
            {
                return NotFound();
            }

            var flat = _flatRepository.Get(id.Value);
            if (flat == null)
            {
                return NotFound();
            }
            else
            {
                Flat = flat;
            }
            return Page();
        }
    }
}
