using SuperCarga.Application.Domain.Jobs.Common.Models;
using SuperCarga.Application.Domain.Proposals.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Customers.Common.Models
{
    public class CustomerFavoriteProposal
    {
        public Guid CustomerId { get; set; }
        public Customer Customer{ get; set; }

        public Guid ProposalId { get; set; }
        public Proposal Proposal { get; set; }
    }
}
