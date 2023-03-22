using MediatR;
using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Customers.Customers.Commands.UploadIdDocument
{
    public class UploadIdDocumentRequest
    {
        public ImageDto IdDocument { get; set; }
    }

    public class UploadIdDocumentCommand : UserRequest<UploadIdDocumentRequest, UploadIdDocumentCommandResponse>
    {
    }
}
