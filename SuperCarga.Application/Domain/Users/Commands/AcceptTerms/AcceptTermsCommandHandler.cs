using MediatR;
using SuperCarga.Application.Domain.Users.Abstraction;

namespace SuperCarga.Application.Domain.Users.Commands.AcceptTerms
{
    public class AcceptTermsCommandHandler : IRequestHandler<AcceptTermsCommand, AcceptTermsCommandResponse>
    {
        private readonly IUsersService usersService;

        public AcceptTermsCommandHandler(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public async Task<AcceptTermsCommandResponse> Handle(AcceptTermsCommand request, CancellationToken cancellationToken)
        {
            var response = new AcceptTermsCommandResponse();

            await usersService.AcceptTerms(request);

            return response;
        }
    }
}
