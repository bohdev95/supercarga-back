﻿using MediatR;
using SuperCarga.Application.Domain.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Users.Commands.AcceptTerms
{
    public class AcceptTermsRequest
    {
    }

    public class AcceptTermsCommand : UserRequest<AcceptTermsRequest, AcceptTermsCommandResponse>
    {
    }
}
