using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Customers.Customers.Queries.Details.Dro
{
    public class CustomerDetailsDto
    {
        public Guid Id { get; set; }

        public DateTime Created { get; set; }

        public decimal Spend { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string ImagePath { get; set; }

        public string? IdDocumentPath { get; set; }

        public string VerifivationState { get; set; }

        public decimal Balance { get; set; }
    }
}
