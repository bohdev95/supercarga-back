using MediatR;
using SuperCarga.Application.Domain.Users.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Common.Model
{
    public interface IUserRequest<TData>
    {
        public User User { get; set; }

        public TData Data { get; set; }
    }

    public class UserRequest<TData, TResponse> : IRequest<TResponse>, IUserRequest<TData>
    {
        public User User { get; set; }

        public TData Data { get; set; }
    }
}
