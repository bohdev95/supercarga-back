using SuperCarga.Application.Domain.Users.Dto;

namespace SuperCarga.Application.Domain.Users.Commands
{
    public class TokensCommandResponse
    {
        public string AccesToken { get; set; }

        public string RefreshToken { get; set; }

        public TokensCommandResponse(TokenDto tokens)
        {
            AccesToken = tokens.AccessToken;
            RefreshToken = tokens.RefreshToken;
        }
    }
}
