using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class UserAccount
    {
        public UserAccount()
        {
            RentContracts = new HashSet<RentContract>();
        }

        public int Id { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public string? Fullname { get; set; }
        public DateTime? DayOfBirth { get; set; }
        public bool? Gender { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? CitizenId { get; set; }
        public DateTime? DateJoin { get; set; }
        public int? Status { get; set; }
        public int RoleUser { get; set; }

        public virtual ICollection<RentContract> RentContracts { get; set; }
    }
}
