using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Jobs.Drivers.Commands.AddToFavorites;
using SuperCarga.Application.Domain.Jobs.Drivers.Commands.RemoveFromFavorites;
using SuperCarga.Application.Domain.Jobs.Drivers.Queries.Details;
using SuperCarga.Application.Domain.Jobs.Drivers.Queries.Details.Dto;
using SuperCarga.Application.Domain.Jobs.Drivers.Queries.List.All;
using SuperCarga.Application.Domain.Jobs.Drivers.Queries.List.Common.Dto;
using SuperCarga.Application.Domain.Jobs.Drivers.Queries.List.Favorites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Jobs.Drivers.Abstraction
{
    public interface IDriverJobsService
    {

        Task<ListResponseDto<DriverJobListItemDto>> ListAllJobs(DriverListAllJobsQuery request);

        Task<ListResponseDto<DriverJobListItemDto>> ListFavoritesJobs(DriverListFavoritesJobsQuery request);

        Task<DriverJobsDetailsDto> GetJobsDetails(GetDriverJobsDetailsQuery request);

        Task AddJobToFavorites(AddJobToFavoritesCommand request);

        Task RemoveJobFromFavorites(RemoveJobFromFavoritesCommand request);
    }
}
