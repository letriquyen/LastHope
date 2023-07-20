using Repository.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Repository.Models
{
    public partial class RentContract
    {
        public RentContract()
        {
            Bills = new HashSet<Bill>();
            Terms = new HashSet<Term>();
        }

        public int Id { get; set; }
        public int? CustomerId { get; set; }
        [Required(ErrorMessage = "Please select the room")]
        public int FlatId { get; set; }
        [Range(0, double.PositiveInfinity, ErrorMessage = "Please enter positive digits")]
        public decimal? Value { get; set; }
        [Required(ErrorMessage = "Please choose the start date")]
        public DateTime? StartDate { get; set; }
        [Required(ErrorMessage = "Please choose the expiry date")]
        public DateTime? ExpiryDate { get; set; }
        public RentContractStatus? Status { get; set; }
        [Required(ErrorMessage = "Please chooose a contract to add")]
        public string? Contract { get; set; }
        [Required(ErrorMessage = "Please input title")]
        public string? Title { get; set; }

        public virtual UserAccount? Customer { get; set; }
        public virtual Flat? Flat { get; set; }
        public virtual ICollection<Bill> Bills { get; set; }
        public virtual ICollection<Term> Terms { get; set; }
    }
}
