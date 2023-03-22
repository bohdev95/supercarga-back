using MediatR;
using SuperCarga.Application.Domain.Drivers.Drivers.Abstraction;
using SuperCarga.Application.Exceptions;

namespace SuperCarga.Application.Domain.Drivers.Drivers.Commands.UploadDrivingLicense
{
    public class UploadDrivingLicenseCommandHandler : IRequestHandler<UploadDrivingLicenseCommand, UploadDrivingLicenseCommandResponse>
    {
        private readonly UploadDrivingLicenseCommandValidator validator;
        private readonly IDriversDriversService driversService;

        public UploadDrivingLicenseCommandHandler(UploadDrivingLicenseCommandValidator validator, IDriversDriversService driversService)
        {
            this.validator = validator;
            this.driversService = driversService;
        }

        public async Task<UploadDrivingLicenseCommandResponse> Handle(UploadDrivingLicenseCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request);
            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            var response = new UploadDrivingLicenseCommandResponse();

            response.DrivingLicensePath = await driversService.UploadDrivingLicense(request);

            return response;
        }
    }
}
