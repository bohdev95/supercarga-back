using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SuperCarga.Application.Domain.Common.Abstraction;
using SuperCarga.Application.Domain.Customers.Common.Models;
using SuperCarga.Application.Domain.Customers.Customers.Abstraction;
using SuperCarga.Application.Domain.Customers.Customers.Commands.UploadIdDocument;
using SuperCarga.Application.Domain.Customers.Customers.Queries.Details;
using SuperCarga.Application.Domain.Customers.Customers.Queries.Details.Dro;
using SuperCarga.Application.Domain.Users.Commands.Register;
using SuperCarga.Application.Domain.Users.Models;
using SuperCarga.Persistence.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Domain.Domain.Customers
{
    public class CustomersCustomersService : CustomersService,  ICustomersCustomersService
    {
        public CustomersCustomersService(SuperCargaContext ctx, IImagesService imagesService) : base(ctx, imagesService)
        {
        }

        public async Task<CustomerDetailsDto> GetCustomersDetails(GetCustomersDetailsQuery request)
        {
            var customer = await ctx.Customers
                .Include(x => x.User)
                .ThenInclude(x => x.Finance)
                .Where(x => x.Id == request.User.CustomerId)
                .Select(x => new CustomerDetailsDto
                {
                    Id = x.Id,
                    Created = x.Created,
                    Spend = 10000, //TODO
                    Name = x.User.DisplayName(),
                    Email = x.User.Email,
                    ImagePath = x.User.ImagePath,
                    IdDocumentPath = x.IdDocumentPath,
                    VerifivationState = x.User.VerificationState,
                    Balance = x.User.Finance.Balance
                })
                .FirstOrDefaultAsync();

            return customer;
        }

        public async Task<Guid> CreateCustomer(RegisterUserCommand command)
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now
            };

            await ctx.Customers.AddAsync(customer);

            var customerRole = await ctx.Roles
                .Where(x => x.Name == Roles.Customer)
                .SingleAsync();

            await CreateUser(command, user =>
            {
                user.CustomerId = customer.Id;
                user.Roles = new List<Role> { customerRole };
            });

            return customer.Id;
        }

        public async Task<string> UploadIdDocument(UploadIdDocumentCommand request)
        {
            var customer = await ctx.Customers
                .Where(x => x.Id == request.User.CustomerId.Value)
                .FirstOrDefaultAsync();

            var idDocumentPath = await imagesService
                .SaveIdDocumentImage(request.Data.IdDocument, request.User.Id);

            customer.IdDocumentPath = idDocumentPath;

            await ctx.SaveChangesAsync();

            return idDocumentPath;
        }
    }
}
