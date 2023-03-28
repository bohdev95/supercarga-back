using SuperCarga.Application.Domain.Common.Model;
using SuperCarga.Application.Domain.Finances.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Users.Models
{
    public class User : Entity
    {
        public string Email { get; set; }

        public string EmailNormalized { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Guid? CustomerId { get; set; }

        public Guid? DriverId { get; set; }

        public string Password { get; set; }

        public bool IsActive { get; set; }

        public List<Role> Roles { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiry { get; set; }

        public bool TermsAccepted { get; set; }

        public string ImagePath { get; set; }

        public string VerificationState { get; set; }

        public Finance Finance { get; set; } 

        public List<Payment> FromPayments { get; set; }

        public List<Payment> ToPayments { get; set; }
    }

    public static class UsersExtensions
    {
        public static string DisplayName(this User user) => $"{user.FirstName} {user.LastName.First()}";
    }
}
