using SuperCarga.Application.Domain.Jobs.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Drivers.Common.Models
{
    public class DriverFavoriteJob
    {
        public Guid DriverId { get; set; }
        public Driver Driver { get; set; }

        public Guid JobId { get; set; }
        public Job Job { get; set; }
    }
}
