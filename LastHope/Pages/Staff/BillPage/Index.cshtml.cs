using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository.Enum;
using Repository.Models;
using Repository.Repository.Implement;
using Repository.Repository.Interface;

namespace LastHope.Pages.Staff.BillPage
{
    public class IndexModel : PageModel
    {
        private readonly IBillRepository _billRepository;

        public IndexModel(IBillRepository billRepository)
        {
            _billRepository = billRepository;
        }

        public IList<Bill> Bill { get;set; } = default!;
        [BindProperty]
        public string CustomerName { get; set; }
        [BindProperty]
        public int PageNumber { get; set; }
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
            if (CustomerName == null)
            {
                CustomerName = "";
            }
            
            CurrentPage = CurrentPage <= 0 ? 1 : CurrentPage;
            Bill = _billRepository.Get(CustomerName, CurrentPage, RecordPerPage,  out int totalPage);
            TotalPage = totalPage;
            Bill = _billRepository.Get();
            return Page();
        }
        public void OnPostSearch()
        {
            if (CustomerName == null)
            {
                CustomerName = "";
            }
            CurrentPage = CurrentPage <= 0 ? 1 : CurrentPage;
            Bill = _billRepository.Get(CustomerName, CurrentPage, RecordPerPage, out int totalPage);
            TotalPage = totalPage;
        }
        public void OnPostPrevious()
        {
            if (CustomerName == null)
            {
                CustomerName = "";
            }
            CurrentPage = Page - 1;
            Bill = _billRepository.Get(CustomerName, RecordPerPage, CurrentPage, out int totalPage);
            TotalPage = totalPage;

        }
        public void OnPostNext()
        {
            if (CustomerName == null)
            {
                CustomerName = "";
            }
            CurrentPage = Page + 1;
            Bill = _billRepository.Get(CustomerName, RecordPerPage, CurrentPage, out int totalPage);
            TotalPage = totalPage;
        }
    }
}
