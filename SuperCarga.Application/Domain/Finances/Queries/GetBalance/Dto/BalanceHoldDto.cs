using SuperCarga.Application.Domain.Drivers.Customers.Dto;

namespace SuperCarga.Application.Domain.Finances.Queries.GetBalance.Dto
{
    public class BalanceHoldDto
    {
        public decimal Value { get; set; }

        public string JobTittle { get; set; }

        public string ContractState { get; set; }

        public CustomerDriverDto Driver { get; set; }
    }
}
