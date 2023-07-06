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
using Repository.Enum;
using OfficeOpenXml;

namespace LastHope.Pages.Staff.BillPage
{
    public class UploadModel : PageModel
    {

        private readonly IBillRepository _billRepository;

        private readonly IBillItemRepository _billItemRepository;
        public IEnumerable<Bill> Bills { get; set; }

        private readonly ILogger<UploadModel> _logger;

        public UploadModel(ILogger<UploadModel> logger, IBillRepository billRepository, IBillItemRepository billItemRepository)
        {
            _logger = logger;
            _billRepository = billRepository;
            _billItemRepository = billItemRepository;
        }


        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("Id") == null || HttpContext.Session.GetInt32("Role") != (int)Role.STAFF)
            {
                return Redirect("/");
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
            ViewData["Message"] = "Create bills and send successfully!";
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
                        if(i >= 3)
                        {
                            var x = reader.GetValue(3);
                            int contractId = int.Parse(reader.GetValue(3).ToString());
                            decimal rentFee = Decimal.Parse( reader.GetValue(4).ToString());
                            decimal electricFee = Decimal.Parse(reader.GetValue(5).ToString());
                            decimal waterFee = Decimal.Parse(reader.GetValue(6).ToString());
                            decimal managementFee = Decimal.Parse(reader.GetValue(7).ToString());
                            decimal parkingFee = Decimal.Parse(reader.GetValue(8).ToString());
                            decimal total = rentFee + electricFee + waterFee + managementFee + parkingFee;


                            string email = reader.GetValue(9).ToString();

                            bill = new Bill()
                            {
                                RentContractId = contractId,
                                Date = DateTime.Now,
                                Value = total,
                                Status = 0,
                                Content = "Monthly bill",
                                Sender = "Staff",
                                Receiver = "Customer",
                                Type = BillType.BILL
                                //rent = reader.GetValue(0).ToString(),
                                //water = reader.GetValue(1).ToString(),
                                //electicity = reader.GetValue(2).ToString(),
                                //management = reader.GetValue(3).ToString(),
                                //parking = reader.GetValue(4).ToString(),
                                //email = reader.GetValue(5).ToString()
                            };
                            bill = _billRepository.AddBill(bill);
                            for (int j = 4; j < 9; j++)
                            {
                                item = new BillItem
                                {
                                    ServiceId = (int)(j - 3),
                                    Quantity = 1,
                                    Value = Decimal.Parse(reader.GetValue(j).ToString()),
                                    BillId = bill.Id
                                };
                                _billItemRepository.Add(item);
                                items.Add(item);
                            }
                            string mainBody = "<tr>" +
                                                    "<th>" + reader.GetValue(4).ToString() + "</th>" +
                                                    "<th>" + reader.GetValue(5).ToString() + "</th>" +
                                                    "<th>" + reader.GetValue(6).ToString() + "</th>" +
                                                    "<th>" + reader.GetValue(7).ToString() + "</th>" +
                                                    "<th>" + reader.GetValue(8).ToString() + "</th>" +
                                                    "<th>" + total + "</th>" +
                                                "</tr>";
                            string body = "<html><head></head><body>This is monthly bill!" +
                    @"<table border=""1"" cellpadding=""5"" style=""border-collapse: collapse;""><tr style=""color:white;background-Color:SkyBlue;font-weight:bold;"">" +
                    "<td>Rent</td><td>Electricity</td><td>Water</td><td>Management</td><td>Parking</td><td>Total</td>" + "</tr>" + mainBody + "</table>Currency unit: VNĐ</body></html>";
                            Email.From.Add(MailboxAddress.Parse("safebuilding.swd@gmail.com"));
                            Email.To.Add(MailboxAddress.Parse(email));
                            Email.Subject = "New Bill ";
                            Email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };
                            using var smtp = new SmtpClient();
                            smtp.Connect("smtp.gmail.com", 465, true);
                            smtp.Authenticate("safebuilding.swd@gmail.com", "tqnvzrldadgobqgy");
                            smtp.Send(Email);
                            smtp.Disconnect(true);

                            list.Add(bill);
                        }
                        
                    }
                }
            }
            return list;
        }

        public IActionResult OnGetExportToExcel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var ws = package.Workbook.Worksheets.Add("Sheet1");
                ws.Cells["A1"].Value = "Issued Date (dd/mm/yyyy):";
                ws.Cells["B1"].Value = DateTime.Now.ToString("dd/MM/yyyy");
                ws.Cells["A2"].Value = "BuildingId";
                ws.Cells["B2"].Value = "BuildingName";
                ws.Cells["C2"].Value = "RoomNumber";
                ws.Cells["D2"].Value = "ContractId";
                ws.Cells["E2"].Value = "Rent Fee";
                ws.Cells["F2"].Value = "Electricity Fee";
                ws.Cells["G2"].Value = "Water Fee";
                ws.Cells["H2"].Value = "Management Fee";
                ws.Cells["I2"].Value = "Parking Fee";
                ws.Cells["J2"].Value = "Email";
                ws.Cells["A:AZ"].AutoFitColumns();
                package.Save();
            }
            stream.Position = 0;
            string excelName = "MonthlyBill.xlsx";

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }

    }
}