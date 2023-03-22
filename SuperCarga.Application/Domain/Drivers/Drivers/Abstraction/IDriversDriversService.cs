using SuperCarga.Application.Domain.Drivers.Drivers.Commands.Edit;
using SuperCarga.Application.Domain.Drivers.Drivers.Commands.UploadDrivingLicense;
using SuperCarga.Application.Domain.Drivers.Drivers.Queries.Details;
using SuperCarga.Application.Domain.Drivers.Drivers.Queries.Details.Dro;
using SuperCarga.Application.Domain.Users.Commands.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Drivers.Drivers.Abstraction
{
    public interface IDriversDriversService
    {
        Task<DriverDetailsDto> GetDriverDetails(GetDriverDetailsQuery request);

        Task EditDriver(EditDriverCommand request);

        Task<Guid> CreateDriver(RegisterUserCommand request, Guid vehivuleId);

        Task<string> UploadDrivingLicense(UploadDrivingLicenseCommand request);
    }
}
