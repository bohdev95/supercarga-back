using SuperCarga.Application.Domain.Finances.Queries.GetBalance;
using SuperCarga.Application.Domain.Finances.Queries.GetBalance.Dto;
using SuperCarga.Application.Domain.Users.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Finances.Abstraction
{
    public interface IFinancesService
    {
        Task<BalanceDto> GetBalance(GetBalanceQuery query);

        void PaymentLock(Action payments);

        void PayFee(Guid fromUserId, Guid contractId, bool save);

        void Pay(Guid fromUserId, Guid toUserId, decimal value, string operation, Guid contractId, bool save);

        void AddHold(Guid userId, decimal value, Guid contractId, bool save);

        decimal RemoveHold(Guid userId, Guid contractId, bool save);

    }
}
