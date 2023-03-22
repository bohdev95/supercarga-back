using SuperCarga.Application.Domain.Common.Model;
using SuperCarga.Application.Domain.Customers.Common.Models;
using SuperCarga.Application.Domain.Drivers.Common.Models;
using SuperCarga.Application.Domain.Jobs.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Proposals.Common.Models
{
    public class Proposal : Entity
    {
        public Guid JobId { get; set; }

        public Job Job { get; set; }

        public Guid DriverId { get; set; }

        public Driver Driver { get; set; }

        public decimal PricePerKm { get; set; }

        public string State { get; set; }

        public bool Checked { get; set; }

        public List<Customer> AddedToFavoriteBy { get; set; }
    }
}
