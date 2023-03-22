using SuperCarga.Application.Domain.Jobs.Common.Models;

namespace SuperCarga.Application.Domain.Common.Dto
{
    public static class CargoDtoExtensions
    {
        public static CargoDto GetCargo(this Job post) => new CargoDto
        {
            Width = post.CargoWidth,
            Height = post.CargoHeight,
            Lenght = post.CargoLenght,
            Weight = post.CargoWeight,
            ImagePath = post.CargoImagePath
        };
    }
}
