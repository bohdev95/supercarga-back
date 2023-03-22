using SuperCarga.Application.Domain.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Users.Models
{
    public static class Roles
    {
        public const string Admin = "ADMIN";
        public const string Customer = "CUSTOMER";
        public const string Driver = "DRIVER";
    }

    public class Role : Entity
    {
        public string Name { get; set; }

        public List<User> Users { get; set; }
    }
}
