using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualBasic;
using Repository.Models;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MimeKit;
using MailKit.Net.Smtp;
using System.Diagnostics.Metrics;
using Repository.Repository.Interface;

namespace SafeBuilding.Pages
{
    public class UploadModel : PageModel
    {

        private readonly IBillRepository _billRepository;


        public IEnumerable<Bill> Bills { get; set; }

        private readonly ILogger<UploadModel> _logger;

        public UploadModel(ILogger<UploadModel> logger, IBillRepository billRepository)
        {
            _logger = logger;
            _billRepository = billRepository;
        }


        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("Id") == null || HttpContext.Session.GetInt32("Role") == 2)
            {
                return RedirectToPage("Pages/Login");
            }
            return Page();
        }


        [HttpGet]
        IActionResult Index(List<Bill> bills = null)
        {
            bills = bills == null ? new List<Bill>() : bills;
            return Page();
        }

        public IActionResult OnPost(IFormFile file, [FromServices] Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            string filename = Path.Combine(hostingEnvironment.WebRootPath, "file", file.FileName);
            using (FileStream fileStream = System.IO.File.Create(filename))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }
            var invoice = this.GetBillList(file.FileName);
            return Page();
        }

        private List<Bill> GetBillList(string fName)
        {
            Bill bill;
            BillItem item;
            List<BillItem> items = new List<BillItem>();
            var Email = new MimeMessage();
            List<Bill> list = new List<Bill>();
            var fileName = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\file"}" + "\\" + fName;
            int i = 0;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelDataReader.ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        i++;
                        if(i >= 2)
                        {   
                            int contractId = Int32.Parse(reader.GetValue(3).ToString());
                            decimal rentFee = Decimal.Parse( reader.GetValue(4).ToString());
                            decimal electricFee = Decimal.Parse(reader.GetValue(5).ToString());
                            decimal waterFee = Decimal.Parse(reader.GetValue(6).ToString());
                            decimal managementFee = Decimal.Parse(reader.GetValue(7).ToString());
                            decimal parkingFee = Decimal.Parse(reader.GetValue(8).ToString());
                            decimal total = Decimal.Parse(reader.GetValue(9).ToString());


                            string email = reader.GetValue(10).ToString();

                            bill = new Bill()
                            {
                                RentContractId = contractId,
                                Date = DateTime.Now,
                                Value = total,
                                Status = 0,
                                Content = "Monthly bill"
                                //rent = reader.GetValue(0).ToString(),
                                //water = reader.GetValue(1).ToString(),
                                //electicity = reader.GetValue(2).ToString(),
                                //management = reader.GetValue(3).ToString(),
                                //parking = reader.GetValue(4).ToString(),
                                //email = reader.GetValue(5).ToString()
                            };

                            for (int j = 4; j < 9; j++)
                            {
                                item = new BillItem
                                {
                                    ServiceId = (int)(j-3),
                                    Quantity = 1,
                                    Value = Decimal.Parse(reader.GetValue(j).ToString())
                                };
                                items.Add(item);
                            }

                            _billRepository.Add(bill, items);
                            string mainBody = "<tr>" +
                                                    "<th>" + reader.GetValue(4).ToString() + "</th>" +
                                                    "<th>" + reader.GetValue(5).ToString() + "</th>" +
                                                    "<th>" + reader.GetValue(6).ToString() + "</th>" +
                                                    "<th>" + reader.GetValue(7).ToString() + "</th>" +
                                                    "<th>" + reader.GetValue(8).ToString() + "</th>" +
                                                    "<th>" + reader.GetValue(9).ToString() + "</th>" +
                                                "</tr>";
                            string body = "<html><head></head><body>" +
                    @"<table border=""1"" cellpadding=""5"" style=""border-collapse: collapse;""><tr style=""color:white;background-Color:SkyBlue;font-weight:bold;"">" +
                    "<td>Rent</td><td>Electricity</td><td>Water</td><td>Management</td><td>Parking</td><td>Total</td>" + "</tr>" + mainBody + "</table></body></html>";
                            Email.From.Add(MailboxAddress.Parse("safebuilding76@gmail.com"));
                            Email.To.Add(MailboxAddress.Parse(email));
                            Email.Subject = "New Invoice ";
                            Email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };
                            using var smtp = new SmtpClient();
                            smtp.Connect("smtp.gmail.com", 465, true);
                            smtp.Authenticate("safebuilding76@gmail.com", "afcllbwpxmsebrnw");
                            smtp.Send(Email);
                            smtp.Disconnect(true);

                            list.Add(bill);
                        }
                        
                    }
                }
            }
            return list;
        }
    }
}