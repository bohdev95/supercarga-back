using SuperCarga.Application.Domain.Common.Model;

namespace SuperCarga.Application.Domain.Jobs.Customers.Commands.Close
{
    public class CloseJobRequest
    {
        public Guid JobId { get; set; }       
    }

    public class CloseJobCommand : UserRequest<CloseJobRequest, CloseJobCommandResponse>
    {
    }
}
