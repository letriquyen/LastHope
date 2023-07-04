using Repository.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Repository.Models
{
    public partial class Bill
    {
        public Bill()
        {
            BillItems = new HashSet<BillItem>();
        }

        [Range(0, double.PositiveInfinity, ErrorMessage = "Please enter positive digits")]
        [Required(ErrorMessage = "Please input information")]
        public int Id { get; set; }

        [Range(0, double.PositiveInfinity, ErrorMessage = "Please enter positive digits")]
        [Required(ErrorMessage = "Please input information")]
        public int? RentContractId { get; set; }


        [Required(ErrorMessage = "Please input information")]
        public DateTime? Date { get; set; }

        [Range(0, double.PositiveInfinity, ErrorMessage = "Please enter positive digits")]
        [Required(ErrorMessage = "Please input information")]
        public decimal? Value { get; set; }

        [Range(0, 1, ErrorMessage = "0 - Not Paid; 1 - Paid")]
        [Required(ErrorMessage = "Please input information")]
        public int? Status { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage = "Only allowed alphabet character")]
        [Required(ErrorMessage = "Please input information")]
        public string? Sender { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage = "Only allowed alphabet character")]
        [Required(ErrorMessage = "Please input information")]
        public string? Receiver { get; set; }


        [Required(ErrorMessage = "Please input information")]
        public string? Content { get; set; }
        //public BillType? Type { get; set; }

        public virtual RentContract? RentContract { get; set; }
        public virtual ICollection<BillItem> BillItems { get; set; }
    }
}
