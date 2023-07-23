using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.Enum;
using Repository.Models;
using Repository.Repository.Interface;

namespace LastHope.Pages.Staff.RentContractPages
{
    public class CreateModel : PageModel
    {
        private readonly IRentContractRepository _rentContractRepository;
        private readonly IFlatRepository _flatRepository;
        private readonly IBuildingRepository _buildingRepository;
        private readonly ITermRepository _termRepository;
        private readonly IUserAccountRepository _userAccountRepository;

        public CreateModel(IRentContractRepository rentContractRepository, IFlatRepository flatRepository, 
            IBuildingRepository buildingRepository, ITermRepository termRepository, 
            IUserAccountRepository userAccountRepository)
        {
            _rentContractRepository = rentContractRepository;
            _flatRepository = flatRepository;
            _buildingRepository = buildingRepository;
            _termRepository = termRepository;
            _userAccountRepository = userAccountRepository;
        }

        public IActionResult OnGet(int? BuildingId)
        {
            if (HttpContext.Session.GetString("Id") == null || HttpContext.Session.GetInt32("Role") != (int)Role.STAFF)
            {
                return Redirect("/");
            }
            this.BuildingId = BuildingId;
            LoadData(BuildingId);

            return Page();
        }

        [BindProperty]
        public RentContract RentContract { get; set; } = default!;
        [BindProperty]
        public List<Term> Terms { get; set; }

        [BindProperty]
        public IFormFile ContractFile { get; set; }
        [BindProperty]
        public int? BuildingId { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid || _rentContractRepository.Get() == null || RentContract == null || ContractFile == null)
            {
                LoadData(BuildingId);
                return Page();
            }
            if (RentContract.StartDate >= RentContract.ExpiryDate)
            {
                ModelState.AddModelError("RentContract.ExpiryDate", "ExpiryDate must be after StartDate");
                LoadData(BuildingId);
                return Page();
            }
            if (ContractFile != null && ContractFile.Length > 0)
            {
                using (var reader = new StreamReader(ContractFile.OpenReadStream()))
                {
                    var filebytes = ReadIFormFileAsByteArray(ContractFile);
                    RentContract.Contract = Convert.ToBase64String(filebytes);
                }
            }
            var contract = _rentContractRepository.Add(RentContract);
            if (contract != null)
            {
                foreach (var term in Terms)
                {
                    term.RentContractId = contract.Id;
                    _termRepository.Add(term);
                }
                if (contract.Customer != null && !string.IsNullOrEmpty(contract.Customer.Email))
                {
                    SendEmail(contract.Customer.Email, contract.Contract, contract.Customer.Fullname);
                }
                    
            }
            
            return RedirectToPage("./Index");
        }
        
       private void LoadData(int? BuildingId)
        {
            ViewData["CustomerId"] = new SelectList(_userAccountRepository.Get(), "Id", "Fullname");
            ViewData["BuildingId"] = new SelectList(_buildingRepository.Get(), "Id", "Name");
            var statuses = Enum.GetValues(typeof(RentContractStatus)).Cast<RentContractStatus>().ToList();
            ViewData["Status"] = new SelectList(statuses.Select((value, index) => new { value, index }), "index", "value");
            if (BuildingId != null)
            {
                ViewData["BuildingId"] = new SelectList(_buildingRepository.Get(), "Id", "Name", BuildingId);
                var flats = _flatRepository.GetByBuilding(BuildingId.Value);
                ViewData["FlatId"] = new SelectList(flats, "Id", "RoomNumber");
            }
        }
        private byte[] ReadIFormFileAsByteArray(IFormFile file)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        private void SendEmail(string email, string contract, string customerName)
        {
            string senderEmail = "safebuilding.swd@gmail.com";
            string senderPassword = "tqnvzrldadgobqgy";

            string recipientEmail = email;

            MailMessage mail = new MailMessage(senderEmail, recipientEmail);

            mail.Subject = "LastHope new Contract";
            mail.IsBodyHtml = true;
            mail.Body = $"<p>You got a new contract in LastHope </p>" +
                $"<a href=\"data:application/octet-stream;base64,{contract}\" download=\"{customerName}.pdf\">Download File</a>";
            
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true; 
            
            smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);

            try
            {
                smtpClient.Send(mail);
                Console.WriteLine("Email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to send email. Error message: " + ex.Message);
            }
        }
    }
}
