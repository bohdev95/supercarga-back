using MediatR;
using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Users.Commands.ChangeImage
{
    public class ChangeImageRequest
    {
        public ImageDto Image { get; set; }
    }

    public class ChangeImageCommand : UserRequest<ChangeImageRequest, ChangeImageCommandResponse>
    {
    }
}
