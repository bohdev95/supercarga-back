using MediatR;
using SuperCarga.Application.Domain.Users.Abstraction;
using SuperCarga.Application.Exceptions;

namespace SuperCarga.Application.Domain.Users.Commands.ChangeImage
{
    public class ChangeImageCommandHandler : IRequestHandler<ChangeImageCommand, ChangeImageCommandResponse>
    {
        private readonly ChangeImageCommandValidator validator;
        private readonly IUsersService usersService;

        public ChangeImageCommandHandler(ChangeImageCommandValidator validator, IUsersService usersService)
        {
            this.validator = validator;
            this.usersService = usersService;
        }

        public async Task<ChangeImageCommandResponse> Handle(ChangeImageCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request);
            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            var response = new ChangeImageCommandResponse();

            response.ImagePath = await usersService.ChangeImage(request);

            return response;
        }
    }
}
