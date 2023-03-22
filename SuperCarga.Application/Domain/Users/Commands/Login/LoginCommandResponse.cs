using SuperCarga.Application.Domain.Users.Dto;

namespace SuperCarga.Application.Domain.Users.Commands.Login
{
    public class LoginCommandResponse : TokensCommandResponse
    {
        public LoginCommandResponse(TokenDto tokens) : base(tokens)
        {
        }
    }
}
