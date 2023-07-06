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
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

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
        [BindProperty]
        public int CurrentPage { get; set; }
        [BindProperty]
        public int TotalPage { get; set; }

        [BindProperty]
        public int Page { get; set; }
        public readonly int RecordPerPage = 5;
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("Id") == null || HttpContext.Session.GetInt32("Role") != (int)Role.STAFF)
            {
                return Redirect("/");
            }
            CurrentPage = CurrentPage <= 0 ? 1 : CurrentPage;
            Flat = _flatRepository.Get(RecordPerPage, CurrentPage, out int totalPage);
            TotalPage = totalPage;
            return Page();
        }

        public void OnPostPrevious()
        {
            CurrentPage = Page - 1;
            Flat = _flatRepository.Get(RecordPerPage, CurrentPage, out int totalPage);
            TotalPage = totalPage;

        }
        public void OnPostNext()
        {
            CurrentPage = Page + 1;
            Flat = _flatRepository.Get(RecordPerPage, CurrentPage, out int totalPage);
            TotalPage = totalPage;
        }
    }
}
