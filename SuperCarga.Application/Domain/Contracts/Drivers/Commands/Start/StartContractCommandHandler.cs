using MediatR;
using SuperCarga.Application.Domain.Contracts.Drivers.Abstraction;
using SuperCarga.Application.Exceptions;

namespace SuperCarga.Application.Domain.Contracts.Drivers.Commands.Start
{
    public class StartContractCommandHandler : IRequestHandler<StartContractCommand, StartContractCommandResponse>
    {
        private readonly StartContractCommandValidator validator;
        private readonly IDriverContractsService driverContractsService;

        public StartContractCommandHandler(StartContractCommandValidator validator, IDriverContractsService driverContractsService)
        {
            this.validator = validator;
            this.driverContractsService = driverContractsService;
        }

        public async Task<StartContractCommandResponse> Handle(StartContractCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request);
            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            var response = new StartContractCommandResponse();

           await driverContractsService.StartContract(request);

            return response;
        }
    }
}
