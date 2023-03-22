using SuperCarga.Application.Domain.Users.Dto;

namespace SuperCarga.Application.Domain.Users.Commands.Refresh
{
    public class RefreshCommandResponse : TokensCommandResponse
    {
        public RefreshCommandResponse(TokenDto tokens) : base(tokens)
        {
        }
    }
}
