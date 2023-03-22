using SuperCarga.Application.Domain.Common.Abstraction;
using SuperCarga.Application.Domain.Customers.Common.Abstraction;
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
    }
}
