namespace SuperCarga.Application.Domain.Contracts.Common.Dto
{
    public class ContractPaymentDto
    {
        public DateTime Date { get; set; }

        public decimal Value { get; set; }

        public string Operation { get; set; }
    }
}
