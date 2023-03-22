using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Jobs.Customers.Commands.Add;
using SuperCarga.Application.Domain.Jobs.Customers.Commands.Close;
using SuperCarga.Application.Domain.Jobs.Customers.Queries.Details;
using SuperCarga.Application.Domain.Jobs.Customers.Queries.Details.Dto;
using SuperCarga.Application.Domain.Jobs.Customers.Queries.List.Active;
using SuperCarga.Application.Domain.Jobs.Customers.Queries.List.Archived;
using SuperCarga.Application.Domain.Jobs.Customers.Queries.List.Common.Dto;

namespace SuperCarga.Application.Domain.Jobs.Customers.Abstraction
{
    public interface ICustomerJobsService
    {
        Task<Guid> AddJob(AddJobCommand request);

        Task CloseJob(CloseJobCommand request);

        Task<ListResponseDto<CustomerJobListItemDto>> ListActiveJobs(CustomerListActiveJobsQuery request);

        Task<ListResponseDto<CustomerJobListItemDto>> ListArchivedJobs(CustomerListArchivedJobsQuery request);

        Task<CustomerJobsDetailsDto> GetJobsDetails(GetCustomerJobsDetailsQuery request);
    }
}
