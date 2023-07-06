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

namespace LastHope.Pages.Staff.FlatPages
{
    public class DeleteModel : PageModel
    {
        private readonly IFlatRepository _flatRepository;

        public DeleteModel(IFlatRepository flatRepository)
        {
            _flatRepository = flatRepository;
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
            else
            {
                Flat = flat;
            }
            return Page();
        }

        public IActionResult OnPost(int? id)
        {
            if (id == null || _flatRepository.Get() == null)
            {
                return NotFound();
            }
            var flat = _flatRepository.Get(id.Value);
            
            if (flat != null)
            {
                flat.Status = FlatStatus.DELETED;
                Flat = flat;
                _flatRepository.Update(flat);
            }

            return RedirectToPage("./Index");
        }
    }
}
