﻿using MediatR;
using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Common.Model;
using SuperCarga.Application.Domain.Jobs.Customers.Queries.List.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Jobs.Customers.Queries.List.Archived
{
    public class CustomerListArchivedJobsRequest : CustomerListJobRequest
    {
    }

    public class CustomerListArchivedJobsQuery : UserRequest<CustomerListArchivedJobsRequest, ListResponseDto<CustomerJobListItemDto>>
    {
    }
}
