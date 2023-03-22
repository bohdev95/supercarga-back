using MediatR;
using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Drivers.Drivers.Commands.UploadDrivingLicense
{
    public class UploadDrivingLicenseRequest
    {
        public ImageDto DrivingLicense { get; set; }
    }

    public class UploadDrivingLicenseCommand : UserRequest<UploadDrivingLicenseRequest, UploadDrivingLicenseCommandResponse>
    {
    }
}
