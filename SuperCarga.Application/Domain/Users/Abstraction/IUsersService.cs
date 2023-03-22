using SuperCarga.Application.Domain.Users.Commands.AcceptTerms;
using SuperCarga.Application.Domain.Users.Commands.ChangeImage;
using SuperCarga.Application.Domain.Users.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Users.Abstraction
{
    public interface IUsersService
    {
        Task<User> Get(string email);

        bool UserExists(string email);

        Task Update(User user);

        Task AcceptTerms(AcceptTermsCommand request);

        Task<string> ChangeImage(ChangeImageCommand request);
    }
}
