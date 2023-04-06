using Microsoft.AspNetCore.Identity;
using SuperCarga.Application.Domain.Users.Abstraction;
using SuperCarga.Application.Domain.Users.Dto;
using SuperCarga.Application.Domain.Users.Models;
using SuperCarga.Application.Exceptions;

namespace SuperCarga.Domain.Domain.Users.Auth
{
    //https://code-maze.com/using-refresh-tokens-in-asp-net-core-authentication/
    //https://jasonwatmore.com/post/2022/01/24/net-6-jwt-authentication-with-refresh-tokens-tutorial-with-example-api

    public class AuthService : IAuthService
    {
        private readonly TokenService tokenService;
        private readonly UserManager<User> userManager;

        public AuthService(TokenService tokenService, UserManager<User> userManager)
        {
            this.tokenService = tokenService;
            this.userManager = userManager;
        }

        public async Task<User> GetUser(string accesToken)
        {
            var principal = tokenService.GetPrincipal(accesToken);
            var userName = principal.Identity.Name;
            var user = await userManager.FindByNameAsync(userName);
            return user;
        }

        public async Task<TokenDto> Login(string email, string password)
        {
            var user = await userManager.FindByNameAsync(email);
            if (user == null || !user.IsActive)
                throw new BadRequestException("Wrong login or password");

            if (!await userManager.CheckPasswordAsync(user, password))
                throw new BadRequestException("Wrong login or password");

            return await GenerateTokensAndUpdateUser(user);
        }

        public async Task<TokenDto> Refresh(string accesToken, string refreshToken)
        {
            var user = await GetUser(accesToken);
            if (user == null || !user.IsActive)
                throw new BadRequestException("User not exists");

            if (user.RefreshToken != refreshToken || user.RefreshTokenExpiry <= DateTime.UtcNow)
                throw new BadRequestException("Invalid client request");

            return await GenerateTokensAndUpdateUser(user);
        }

        private async Task<TokenDto> GenerateTokensAndUpdateUser(User user)
        {
            var tokens = tokenService.GenerateTokens(user);

            user.RefreshToken = tokens.RefreshToken;
            user.RefreshTokenExpiry = tokens.RefreshTokenExpiry;
            await userManager.UpdateAsync(user);

            return tokens;
        }

    }
}
