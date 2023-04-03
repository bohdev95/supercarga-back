using SuperCarga.Application.Domain.Common.Model;
using SuperCarga.Application.Domain.Contracts.Common.Models;
using SuperCarga.Application.Domain.Jobs.Common.Models;
using SuperCarga.Application.Domain.Users.Models;
using SuperCarga.Application.Domain.VehiculeTypes.Models;


namespace SuperCarga.Application.Domain.Drivers.Common.Models
{
    public class Driver : Entity
    {
        public User User { get; set; }

        public Guid VehiculeTypeId { get; set; }

        public VehiculeType VehiculeType { get; set; }

        public List<Job> FavoriteJobs { get; set; }

        public string? DrivingLicensePath { get; set; }

        public int Contracts { get; set; }

        public decimal Earnings { get; set; }

        public int RatedContracts { get; set; }

        public decimal? Rating { get; set; }
    }
}
