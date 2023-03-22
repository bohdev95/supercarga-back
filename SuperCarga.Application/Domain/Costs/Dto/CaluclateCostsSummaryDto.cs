namespace SuperCarga.Application.Domain.Costs.Dto
{
    public class CaluclateCostsSummaryDto
    {
        public decimal PricePerKm { get; set; }

        public int Distance { get; set; }

        public bool RequireLoadingCrew { get; set; }

        public bool RequireUnloadingCrew { get; set; }
    }
}
