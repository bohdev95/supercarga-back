using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Customers.Common.Dto
{
    public class TopCustomersDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string ImagePath { get; set; }

        public int Hires { get; set; }

        public decimal Spends { get; set; }
    }
}
