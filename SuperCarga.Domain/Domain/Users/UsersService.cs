using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SuperCarga.Application.Domain.Common.Abstraction;
using SuperCarga.Application.Domain.Finances.Model;
using SuperCarga.Application.Domain.Users.Abstraction;
using SuperCarga.Application.Domain.Users.Commands.AcceptTerms;
using SuperCarga.Application.Domain.Users.Commands.ChangeImage;
using SuperCarga.Application.Domain.Users.Commands.Register;
using SuperCarga.Application.Domain.Users.Models;
using SuperCarga.Persistence.Database;

namespace SuperCarga.Domain.Domain.Users
{
    public class UsersService : IUsersService
    {
        protected readonly SuperCargaContext ctx;
        protected readonly IImagesService imagesService;

        public UsersService(SuperCargaContext ctx, IImagesService imagesService)
        {
            this.ctx = ctx;
            this.imagesService = imagesService;
        }

        public async Task<User> Get(string email)
        {
            var normalizedEmail = email.ToUpper();
            var user = await ctx.Users
                .Include(x => x.Roles)
                .Where(x => x.EmailNormalized == normalizedEmail)
                .FirstOrDefaultAsync();

            return user;
        }

        public bool UserExists(string email) => ctx.Users.Where(x => x.EmailNormalized == email.ToUpper()).Any();

        public async Task Update(User user)
        {
            ctx.Users.Update(user);

            await ctx.SaveChangesAsync();
        }

        public async Task AcceptTerms(AcceptTermsCommand request)
        {
            var user = await ctx.Users
                .Where(x => x.Id == request.User.Id)
                .FirstOrDefaultAsync();

            user.TermsAccepted = true;

            await ctx.SaveChangesAsync();
        }

        public async Task<string> ChangeImage(ChangeImageCommand request)
        {
            var user = await ctx.Users
                .Where(x => x.Id == request.User.Id)
                .FirstOrDefaultAsync();

            var imagePath = await imagesService
                .SaveUserImage(request.Data.Image, request.User.Id);

            user.ImagePath = imagePath;

            await ctx.SaveChangesAsync();

            return imagePath;
        }

        public async Task CreateUser(RegisterUserCommand request, Action<User> setUserData)
        {
            var userId = Guid.NewGuid();

            var imagePath = await imagesService.SaveUserImage(request.Image, userId);

            var user = new User
            {
                Id = userId,
                Created = DateTime.Now,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                TermsAccepted = false,
                IsActive = true,
                ImagePath = imagePath,
                VerificationState = VerificationState.Verifed //TODO
            };

            user.EmailNormalized = user.Email.ToUpper();
            user.Password = new PasswordHasher<User>().HashPassword(user, request.Password);

            var finance = new Finance
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                UserId = user.Id,
                Balance = 0
            };

            setUserData(user);

            await ctx.Users.AddAsync(user);
            await ctx.Finances.AddAsync(finance);
            await ctx.SaveChangesAsync();
        }
    }
}
