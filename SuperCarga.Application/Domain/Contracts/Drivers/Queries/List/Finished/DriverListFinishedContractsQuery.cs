﻿using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Common.Model;
using SuperCarga.Application.Domain.Contracts.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Contracts.Drivers.Queries.List.Finished
{
    public class DriverListFinishedContractsRequest : ListContractRequest
    {
    }

    public class DriverListFinishedContractsQuery : UserRequest<DriverListFinishedContractsRequest, ListResponseDto<FinishedContractListITemDto>>
    {
    }
}
