using MediatR;
using SuperCarga.Application.Domain.Jobs.Customers.Abstraction;
using SuperCarga.Application.Exceptions;

namespace SuperCarga.Application.Domain.Jobs.Customers.Commands.Close
{
    public class CloseJobCommandHandler : IRequestHandler<CloseJobCommand, CloseJobCommandResponse>
    {
        private readonly CloseJobCommandValidator validator;
        private readonly ICustomerJobsService customerJobsService;

        public CloseJobCommandHandler(CloseJobCommandValidator validator, ICustomerJobsService customerJobsService)
        {
            this.validator = validator;
            this.customerJobsService = customerJobsService;
        }

        public async Task<CloseJobCommandResponse> Handle(CloseJobCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request);
            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            var response = new CloseJobCommandResponse();

            await customerJobsService.CloseJob(request);

            return response;
        }

    }
}
