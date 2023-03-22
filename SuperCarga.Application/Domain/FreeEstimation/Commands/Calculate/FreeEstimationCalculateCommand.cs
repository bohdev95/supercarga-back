using MediatR;
using SuperCarga.Application.Domain.Common.Dto;

namespace SuperCarga.Application.Domain.FreeEstimation.Commands.Calculate
{
    public class FreeEstimationCalculateCommand : IRequest<FreeEstimationCalculateCommandResponse>
    {
        public CargoDimensionsDto Cargo { get; set; }
        
        public int EstimatedDistance { get; set; }

        public bool RequireLoadingCrew { get; set; }

        public bool RequireUnloadingCrew { get; set; }

        public string Email { get; set; }

        public string CustomerName { get; set; }
    }
}
