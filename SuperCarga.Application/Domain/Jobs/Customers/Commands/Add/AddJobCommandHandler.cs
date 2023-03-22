using MediatR;
using SuperCarga.Application.Domain.Jobs.Customers.Abstraction;
using SuperCarga.Application.Exceptions;

namespace SuperCarga.Application.Domain.Jobs.Customers.Commands.Add
{
    public class AddJobCommandHandler : IRequestHandler<AddJobCommand, AddJobCommandResponse>
    {
        private readonly AddJobCommandValidator validator;
        private readonly ICustomerJobsService customerJobsService;

        public AddJobCommandHandler(AddJobCommandValidator validator, ICustomerJobsService customerJobsService)
        {
            this.validator = validator;
            this.customerJobsService = customerJobsService;
        }

        public async Task<AddJobCommandResponse> Handle(AddJobCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request);
            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            var response = new AddJobCommandResponse();

            response.Id = await customerJobsService.AddJob(request);

            return response;
        }

    }
}
