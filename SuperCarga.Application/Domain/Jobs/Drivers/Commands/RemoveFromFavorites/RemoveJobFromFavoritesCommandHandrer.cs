using MediatR;
using SuperCarga.Application.Domain.Jobs.Drivers.Abstraction;
using SuperCarga.Application.Exceptions;

namespace SuperCarga.Application.Domain.Jobs.Drivers.Commands.RemoveFromFavorites
{
    public class RemoveJobFromFavoritesCommandHandrer : IRequestHandler<RemoveJobFromFavoritesCommand, RemoveJobFromFavoritesCommandResponse>
    {
        private readonly RemoveJobFromFavoritesCommandValidator validator;
        private readonly IDriverJobsService driverJobsService;

        public RemoveJobFromFavoritesCommandHandrer(RemoveJobFromFavoritesCommandValidator validator, IDriverJobsService driverJobsService)
        {
            this.validator = validator;
            this.driverJobsService = driverJobsService;
        }

        public async Task<RemoveJobFromFavoritesCommandResponse> Handle(RemoveJobFromFavoritesCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request);
            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            var response = new RemoveJobFromFavoritesCommandResponse();

            await driverJobsService.RemoveJobFromFavorites(request);

            return response;
        }
    }
}
