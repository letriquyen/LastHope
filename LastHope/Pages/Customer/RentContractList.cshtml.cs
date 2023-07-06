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

namespace LastHope.Pages.Customer
{
    public class RentContractListModel : PageModel
    {
        private readonly IRentContractRepository _rentContractRepository;

        public RentContractListModel(IRentContractRepository rentContractRepository)
        {
            _rentContractRepository = rentContractRepository;
            
        }

        public IList<RentContract> RentContract { get; set; } = default!;
        [BindProperty]
        public int CurrentPage { get; set; }
        [BindProperty]
        public int TotalPage { get; set; }

        [BindProperty]
        public int Page { get; set; }
        [BindProperty]
        public string Search { get; set; }
        public readonly int RecordPerPage = 5;
        private int _customerId; 
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("Id") == null || HttpContext.Session.GetInt32("Role") != (int)Role.CUSTOMER)
            {
                return Redirect("/");
            }
            _customerId = HttpContext.Session.GetInt32("Id").Value;
            if (Search == null)
            {
                Search = "";
            }
            CurrentPage = CurrentPage <= 0 ? 1 : CurrentPage;
            RentContract = _rentContractRepository.Get(_customerId, RecordPerPage, CurrentPage, out int totalPage);
            TotalPage = totalPage;
            return Page();
        }
       
        public void OnPostPrevious()
        {
            if (Search == null)
            {
                Search = "";
            }
            CurrentPage = Page - 1;
            RentContract = _rentContractRepository.Get(_customerId, RecordPerPage, CurrentPage, out int totalPage);
            TotalPage = totalPage;

        }
        public void OnPostNext()
        {
            if (Search == null)
            {
                Search = "";
            }
            CurrentPage = Page + 1;
            RentContract = _rentContractRepository.Get(_customerId, RecordPerPage, CurrentPage, out int totalPage);
            TotalPage = totalPage;
        }
    }
}
