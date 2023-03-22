using Microsoft.AspNetCore.Identity;
using SuperCarga.Application.Domain.Users.Abstraction;
using SuperCarga.Application.Domain.Users.Models;

namespace SuperCarga.Domain.Domain.Users.Auth
{
    public class UserStore : IUserStore<User>, IUserPasswordStore<User>
    {
        private readonly IUsersService usersService;

        public UserStore(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {

        }

        public Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken) => await usersService.Get(normalizedUserName);

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken) => Task.FromResult(user.EmailNormalized);

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken) => Task.FromResult(user.Password);

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken) => Task.FromResult(user.Id.ToString());

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken) => Task.FromResult(user.Email);

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken) => Task.FromResult(user.EmailNormalized = normalizedName);

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            await usersService.Update(user);

            return IdentityResult.Success;
        }
    }
}
