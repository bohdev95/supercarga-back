using SuperCarga.Application.Domain.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Contracts.Common.Dto
{
    public class ListContractRequest : ListRequestDto
    {
        public DateTime? CreatedFrom { get; set; }

        public DateTime? CreatedTo { get; set; }

        public string? State { get; set; }

        public string? Search { get; set; }
    }
}
