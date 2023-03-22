using MediatR;
using SuperCarga.Application.Domain.Users.Abstraction;
using SuperCarga.Application.Exceptions;

namespace SuperCarga.Application.Domain.Users.Commands.Refresh
{
    public class RefreshCommandHandler : IRequestHandler<RefreshCommand, RefreshCommandResponse>
    {
        private readonly RefreshCommandValidator validator;
        private readonly IAuthService authService;

        public RefreshCommandHandler(RefreshCommandValidator validator, IAuthService authService)
        {
            this.validator = validator;
            this.authService = authService;
        }

        public async Task<RefreshCommandResponse> Handle(RefreshCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request);
            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            var tokens = await authService.Refresh(request.AccesToken, request.RefreshToken);

            var response = new RefreshCommandResponse(tokens);

            return response;
        }

    }
}
