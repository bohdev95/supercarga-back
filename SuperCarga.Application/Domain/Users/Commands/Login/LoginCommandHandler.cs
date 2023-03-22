using MediatR;
using SuperCarga.Application.Domain.Users.Abstraction;
using SuperCarga.Application.Exceptions;

namespace SuperCarga.Application.Domain.Users.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResponse>
    {
        private readonly LoginCommandValidator validator;
        private readonly IAuthService authService;

        public LoginCommandHandler(LoginCommandValidator validator, IAuthService authService)
        {
            this.validator = validator;
            this.authService = authService;
        }

        public async Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request);
            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            var tokens = await authService.Login(request.Email, request.Password);

            var response = new LoginCommandResponse(tokens);

            return response;
        }

    }
}
