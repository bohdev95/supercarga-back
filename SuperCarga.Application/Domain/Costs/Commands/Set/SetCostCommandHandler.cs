using MediatR;
using SuperCarga.Application.Domain.Costs.Abstraction;
using SuperCarga.Application.Exceptions;

namespace SuperCarga.Application.Domain.Costs.Commands.Set
{
    public class SetCostCommandHandler : IRequestHandler<SetCostCommand, SetCostCommandResponse>
    {
        private readonly SetCostCommandValidator validator;
        private readonly ICostsService costsService;

        public SetCostCommandHandler(SetCostCommandValidator validator, ICostsService costsService)
        {
            this.validator = validator;
            this.costsService = costsService;
        }

        public async Task<SetCostCommandResponse> Handle(SetCostCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request);
            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            var response = new SetCostCommandResponse();

            await costsService.SetCost(request);

            return response;
        }
    }
}
