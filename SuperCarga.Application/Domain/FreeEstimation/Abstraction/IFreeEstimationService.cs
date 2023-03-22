using SuperCarga.Application.Domain.FreeEstimation.Commands.Calculate;
using SuperCarga.Application.Domain.FreeEstimation.Commands.Calculate.Dto;

namespace SuperCarga.Application.Domain.FreeEstimation.Abstraction
{
    public interface IFreeEstimationService
    {
        Task<FreeEstimationCalculateDto> Calculate(FreeEstimationCalculateCommand request);
    }
}
