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
    public class IndexModel : PageModel
    {
        private readonly Repository.Models.LastHopeDatabaseContext _context;

        public IndexModel(Repository.Models.LastHopeDatabaseContext context)
        {
            _context = context;
        }

        public IList<Flat> Flat { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Flats != null)
            {
                Flat = await _context.Flats
                .Include(f => f.Building)
                .Include(f => f.FlatType).ToListAsync();
            }
        }
    }
}
