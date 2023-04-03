using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Drivers.Common.Dto
{
    public class TopRatedDriverDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string ImagePath { get; set; }

        public decimal Earnings { get; set; }

        public decimal Rating { get; set; }
    }
}
