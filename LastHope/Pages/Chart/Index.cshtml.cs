using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Models;
using Repository.Repository.Implement;
using Repository.Repository.Interface;

namespace LastHope.Pages.Chart
{
    public class IndexModel : PageModel
    {
        public IRentContractRepository _contractRepository = new RentContractRepository();
        public IBuildingRepository _buildingRepository = new BuildingRepository();
        public IFlatRepository _flatRepository = new FlatRepository();
        public IBillRepository _billRepository = new BillRepository();

        [BindProperty]
        public int contractYear { get; set; }
        public IDictionary<int, int> contracts = new Dictionary<int, int>();
        public IList<Building> buildings = new List<Building>();
        public IDictionary<int, int> bills = new Dictionary<int, int>();
        public IDictionary<string, int> typeOfBills = new Dictionary<string, int>();
        [BindProperty]
        public int buildingId { get; set; }

        public async Task<IActionResult> OnGet()
        {
            //if (HttpContext.Session.GetString("Phone") == null)
            //{
            //    return RedirectToPage("Login");
            //}
            buildings = _buildingRepository.Get();

            for (int i = 1; i <= 12; i++)
            {
                contracts.Add(i, 0);
                bills.Add(i, 0);

            }
            typeOfBills.Add("Unpaid", 0);
            typeOfBills.Add("Paid", 0);

            var contractList = _contractRepository.Get();
            List<RentContract> currentYearContracts = new List<RentContract>();
            foreach (var contract in contractList)
            {
                if (contract.StartDate.Value.Year == DateTime.Now.Year)
                    currentYearContracts.Add(contract);
            }

            List<Bill> billList = _billRepository.Get();
            List<Bill> currentYearBills = new List<Bill>();
            foreach (var bill in billList)
            {
                if (bill.Date.Value.Year == DateTime.Now.Year)
                {
                    currentYearBills.Add(bill);
                }
            }

            for (int i = 1; i <= 12; i++)
            {
                foreach (var contract in currentYearContracts)
                {
                    if (contract.StartDate.Value.Month == i)
                        contracts[i]++;
                }
            }

            for (int i = 1; i <= 12; i++)
            {
                foreach (var bill in currentYearBills)
                {
                    if (bill.Date.Value.Month == i)
                        bills[i]++;
                }
            }

            foreach (var bill in currentYearBills)
            {
                if (bill.Status == 0)
                    typeOfBills["Unpaid"]++;
                else typeOfBills["Paid"]++;
            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            buildings = _buildingRepository.Get();

            for (int i = 1; i <= 12; i++)
            {
                contracts.Add(i, 0);
                bills.Add(i, 0);
            }
            typeOfBills.Add("Unpaid", 0);
            typeOfBills.Add("Paid", 0);

            var contractList = _contractRepository.Get();
            List<RentContract> filteredContracts = new List<RentContract>();
            foreach (var contract in contractList)
            {
                if (contract.StartDate.Value.Year == contractYear)
                    filteredContracts.Add(contract);
            }

            List<Flat> flatList = new List<Flat>();
            if (buildingId != 0)
            {
                flatList = _flatRepository.GetByBuilding(buildingId);
                List<RentContract> temp = new List<RentContract>();
                Flat flat = new Flat();
                foreach (var contract in filteredContracts)
                {
                    flat = _flatRepository.Get(contract.FlatId);
                    if (!flatList.Contains(flat))
                    {
                        temp.Add(contract);
                    }
                }
                foreach (var contract in temp)
                {
                    filteredContracts.Remove(contract);
                }
            }
            for (int i = 1; i <= 12; i++)
            {
                foreach (var contract in filteredContracts)
                {
                    if (contract.StartDate.Value.Month == i)
                        contracts[i]++;
                }
            }

            List<Bill> billList = _billRepository.Get();
            List<Bill> tempBill = new List<Bill>();
            RentContract tempContract = new RentContract();
            foreach (var bill in billList)
            {
                tempContract = _contractRepository.Get(bill.RentContractId);
                if (!filteredContracts.Contains(tempContract))
                {
                    tempBill.Add(bill);
                }
            }
            foreach (var bill in tempBill)
            {
                billList.Remove(bill);
            }
            for (int i = 1; i <= 12; i++)
            {
                foreach (var bill in billList)
                {
                    if (bill.Date.Value.Month == i)
                        bills[i]++;
                }
            }
            foreach (var bill in billList)
            {
                if (bill.Status == 0)
                    typeOfBills["Unpaid"]++;
                else typeOfBills["Paid"]++;
            }


            return Page();
        }
    }
}
