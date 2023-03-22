using MediatR;

namespace SuperCarga.Application.Domain.Costs.Commands.Set
{
    public class SetCostCommand : IRequest<SetCostCommandResponse>
    {
        public string Type { get; set; }

        public decimal Value { get; set; }

        public DateTime FromDate { get; set; }
    }
}
