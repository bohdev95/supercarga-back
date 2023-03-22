using MediatR;
using SuperCarga.Application.Domain.Proposals.Drivers.Abstraction;
using SuperCarga.Application.Exceptions;

namespace SuperCarga.Application.Domain.Proposals.Drivers.Commands.Add
{
    public class AddProposalCommandHandler : IRequestHandler<AddProposalCommand, AddProposalCommandResponse>
    {
        private readonly AddProposalCommandValidator validator;
        private readonly IDriverProposalsService driverProposalsService;

        public AddProposalCommandHandler(AddProposalCommandValidator validator, IDriverProposalsService driverProposalsService)
        {
            this.validator = validator;
            this.driverProposalsService = driverProposalsService;
        }

        public async Task<AddProposalCommandResponse> Handle(AddProposalCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request);
            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            var response = new AddProposalCommandResponse();

            response.Id = await driverProposalsService.AddProposal(request);

            return response;
        }
    }
}
