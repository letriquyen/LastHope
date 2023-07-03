﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using Repository.Repository.Interface;

namespace LastHope.Pages.Staff.RentContractPages
{
    public class IndexModel : PageModel
    {
        private readonly IRentContractRepository _rentContractRepository;

        public IndexModel(IRentContractRepository rentContractRepository)
        {
            _rentContractRepository = rentContractRepository;
        }

        public IList<RentContract> RentContract { get;set; } = default!;

        public void OnGet()
        {
            RentContract = _rentContractRepository.Get();
        }
    }
}
