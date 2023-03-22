using MediatR;
using SuperCarga.Application.Domain.Jobs.Drivers.Abstraction;
using SuperCarga.Application.Exceptions;

namespace SuperCarga.Application.Domain.Jobs.Drivers.Commands.AddToFavorites
{
    public class AddJobToFavoritesCommandHandrer : IRequestHandler<AddJobToFavoritesCommand, AddJobToFavoritesCommandResponse>
    {
        private readonly AddJobToFavoritesCommandValidator validator;
        private readonly IDriverJobsService driverJobsService;

        public AddJobToFavoritesCommandHandrer(AddJobToFavoritesCommandValidator validator, IDriverJobsService driverJobsService)
        {
            this.validator = validator;
            this.driverJobsService = driverJobsService;
        }

        public async Task<AddJobToFavoritesCommandResponse> Handle(AddJobToFavoritesCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request);
            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            var response = new AddJobToFavoritesCommandResponse();

            await driverJobsService.AddJobToFavorites(request);

            return response;
        }
    }
}
