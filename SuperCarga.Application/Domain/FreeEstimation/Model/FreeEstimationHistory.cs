using SuperCarga.Application.Domain.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.FreeEstimation.Model
{
    public class FreeEstimationHistory : Entity
    {
        #region cargo

        public decimal CargoWeight { get; set; }

        public decimal CargoWidth { get; set; }

        public decimal CargoHeight { get; set; }

        public decimal CargoLenght { get; set; }

        #endregion

        public int EstimatedDistance { get; set; }

        public bool RequireLoadingCrew { get; set; }

        public bool RequireUnloadingCrew { get; set; }

        public string Email { get; set; }

        public string CustomerName { get; set; }

        public Guid ResultVehiculeTypeId { get; set; }

        public decimal ResultPricePerKm { get; set; }

        public decimal ResultEstimatedCost { get; set; }
    }
}
