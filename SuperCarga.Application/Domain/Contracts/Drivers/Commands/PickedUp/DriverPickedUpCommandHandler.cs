using MediatR;
using SuperCarga.Application.Domain.Contracts.Drivers.Abstraction;
using SuperCarga.Application.Exceptions;

namespace SuperCarga.Application.Domain.Contracts.Drivers.Commands.PickedUp
{
    public class DriverPickedUpCommandHandler : IRequestHandler<DriverPickedUpCommand, DriverPickedUpCommandResponse>
    {
        private readonly DriverPickedUpCommandValidator validator;
        private readonly IDriverContractsService driverContractsService;

        public DriverPickedUpCommandHandler(DriverPickedUpCommandValidator validator, IDriverContractsService driverContractsService)
        {
            this.validator = validator;
            this.driverContractsService = driverContractsService;
        }

        public async Task<DriverPickedUpCommandResponse> Handle(DriverPickedUpCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request);
            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            var response = new DriverPickedUpCommandResponse();

            await driverContractsService.PickedUp(request);

            return response;
        }
    }
}
