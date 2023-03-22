using MediatR;
using SuperCarga.Application.Domain.FreeEstimation.Abstraction;
using SuperCarga.Application.Exceptions;

namespace SuperCarga.Application.Domain.FreeEstimation.Commands.Calculate
{
    public class FreeEstimationCalculateCommandHandler : IRequestHandler<FreeEstimationCalculateCommand, FreeEstimationCalculateCommandResponse>
    {
        private readonly FreeEstimationCalculateCommandValidator validator;
        private readonly IFreeEstimationService freeEstimationService;

        public FreeEstimationCalculateCommandHandler(FreeEstimationCalculateCommandValidator validator, IFreeEstimationService freeEstimationService)
        {
            this.validator = validator;
            this.freeEstimationService = freeEstimationService;
        }

        public async Task<FreeEstimationCalculateCommandResponse> Handle(FreeEstimationCalculateCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request);
            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            var response = new FreeEstimationCalculateCommandResponse();

            response.Estimation = await freeEstimationService.Calculate(request);

            return response;
        }
    }
}
