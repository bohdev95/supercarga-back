using MediatR;
using SuperCarga.Application.Domain.Contracts.Drivers.Abstraction;
using SuperCarga.Application.Exceptions;

namespace SuperCarga.Application.Domain.Contracts.Drivers.Commands.Delivered
{
    public class DriverDeliveredCommandHandler : IRequestHandler<DriverDeliveredCommand, DriverDeliveredCommandResponse>
    {
        private readonly DriverDeliveredCommandValidator validator;
        private readonly IDriverContractsService driverContractsService;

        public DriverDeliveredCommandHandler(DriverDeliveredCommandValidator validator, IDriverContractsService driverContractsService)
        {
            this.validator = validator;
            this.driverContractsService = driverContractsService;
        }

        public async Task<DriverDeliveredCommandResponse> Handle(DriverDeliveredCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request);
            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            var response = new DriverDeliveredCommandResponse();

            await driverContractsService.Delivered(request);

            return response;
        }
    }
}
