using SuperCarga.Application.Domain.Users.Dto;
using SuperCarga.Application.Domain.Users.Models;

namespace SuperCarga.Application.Domain.Users.Abstraction
{
    public interface IAuthService
    {
        Task<User> GetUser(string accesToken);

        Task<TokenDto> Login(string email, string password);

        Task<TokenDto> Refresh(string accesToken, string refreshToken);
    }
}
