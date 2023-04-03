using Microsoft.EntityFrameworkCore;
using SuperCarga.Application.Domain.Common.Abstraction;
using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Customers.Common.Abstraction;
using SuperCarga.Application.Domain.Customers.Common.Dto;
using SuperCarga.Domain.Domain.Common;
using SuperCarga.Domain.Domain.Users;
using SuperCarga.Persistence.Database;

namespace SuperCarga.Domain.Domain.Customers
{
    public class CustomersService : UsersService, ICustomersService
    {
        public CustomersService(SuperCargaContext ctx, IImagesService imagesService) : base(ctx, imagesService)
        {
        }

        public bool CustomerExists(Guid id) => ctx.Customers.Where(x => x.Id == id).Any();

        public async Task<ListResponseDto<TopCustomersDto>> GetTopCustomers(ListRequestDto request)
        {
            var customers = await ctx.Customers
                .Include(x => x.User)
                .Select(x => new TopCustomersDto
                {
                    Id = x.Id,
                    Name = x.User.FirstName,
                    LastName = x.User.LastName,
                    ImagePath = x.User.ImagePath,
                    Hires = x.FinalizedContracts,
                    Spends = x.Spends
                })
                .OrderByDescending(x => x.Spends)
                .Paginate(request);

            return customers;
        }
    }
}
