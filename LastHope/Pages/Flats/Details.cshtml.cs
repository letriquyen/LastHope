using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository.Models;

namespace LastHope.Pages.Flats
{
    public class DetailsModel : PageModel
    {
        private readonly Repository.Models.LastHopeDatabaseContext _context;

        public DetailsModel(Repository.Models.LastHopeDatabaseContext context)
        {
            _context = context;
        }

      public Flat Flat { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Flats == null)
            {
                return NotFound();
            }

            var flat = await _context.Flats.FirstOrDefaultAsync(m => m.Id == id);
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
