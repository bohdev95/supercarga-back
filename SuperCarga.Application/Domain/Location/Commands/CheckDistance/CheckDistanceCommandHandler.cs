using MediatR;
using SuperCarga.Application.Domain.Location.Abstraction;
using SuperCarga.Application.Exceptions;

namespace SuperCarga.Application.Domain.Location.Commands.CheckDistance
{
    public class CheckDistanceCommandHandler : IRequestHandler<CheckDistanceCommand, CheckDistanceCommandResponse>
    {
        private readonly CheckDistanceCommandValidator validator;
        private readonly IDistanceService distanceService;

        public CheckDistanceCommandHandler(CheckDistanceCommandValidator validator, IDistanceService distanceService)
        {
            this.validator = validator;
            this.distanceService = distanceService;
        }

        public async Task<CheckDistanceCommandResponse> Handle(CheckDistanceCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request);
            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            var response = new CheckDistanceCommandResponse();

            response.Distance = await distanceService.CheckDistance(request.Origin, request.Destination);

            return response;
        }
    }
}
