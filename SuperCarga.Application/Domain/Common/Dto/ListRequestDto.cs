using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Common.Dto
{
    public class ListRequestDto
    {
        public int? Page { get; set; }

        public int? PageSize { get; set; }
    }
}
