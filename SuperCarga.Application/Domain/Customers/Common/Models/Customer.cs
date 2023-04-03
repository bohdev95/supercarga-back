using SuperCarga.Application.Domain.Common.Model;
using SuperCarga.Application.Domain.Proposals.Common.Models;
using SuperCarga.Application.Domain.Users.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Customers.Common.Models
{
    public class Customer : Entity
    {
        public User User { get; set; }

        public List<Proposal> FavoriteProposals { get; set; }

        public string? IdDocumentPath { get; set; }

        public decimal Spends { get; set; }

        public int FinalizedContracts { get; set; }
    }
}
