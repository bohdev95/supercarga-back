using SuperCarga.Application.Domain.Jobs.Common.Models;

namespace SuperCarga.Application.Domain.Location.Dto
{
    public static class AdressDtoExtesnisons
    {
        public static AddressDto GetOrigin(this Job post) => new AddressDto
        {
            City = post.OriginCity,
            Street = post.OriginStreet,
            PostCode = post.OriginPostCode
        };

        public static AddressDto GetDestination(this Job post) => new AddressDto
        {
            City = post.DestinationCity,
            Street = post.DestinationStreet,
            PostCode = post.DestinationPostCode
        };
    }
}
