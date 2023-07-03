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
    public class IndexModel : PageModel
    {
        private readonly IFlatRepository _flatRepository;

        public IndexModel(IFlatRepository flatRepository)
        {
            _flatRepository = flatRepository;
        }

        public IList<Flat> Flat { get;set; } = default!;

        public void OnGet()
        {
            Flat = _flatRepository.Get();
        }
    }
}
