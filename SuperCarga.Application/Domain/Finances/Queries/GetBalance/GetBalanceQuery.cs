﻿using SuperCarga.Application.Domain.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Finances.Queries.GetBalance
{
    public class GetBalanceRequest
    {
    }

    public class GetBalanceQuery : UserRequest<GetBalanceRequest, GetBalanceQueryResponse>
    {
    }
}
