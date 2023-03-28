using Microsoft.EntityFrameworkCore;
using SuperCarga.Application.Domain.Drivers.Customers.Dto;
using SuperCarga.Application.Domain.Finances.Abstraction;
using SuperCarga.Application.Domain.Finances.Model;
using SuperCarga.Application.Domain.Finances.Queries.GetBalance;
using SuperCarga.Application.Domain.Finances.Queries.GetBalance.Dto;
using SuperCarga.Application.Domain.Users.Models;
using SuperCarga.Application.Exceptions;
using SuperCarga.Persistence.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Domain.Domain.Finances
{
    public class FinancesService : IFinancesService
    {
        private readonly SuperCargaContext ctx;

        public FinancesService(SuperCargaContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<BalanceDto> GetBalance(GetBalanceQuery query)
        {
            var dto = await ctx.Finances
                .Include(x => x.Holds)
                .ThenInclude(x => x.RelatedContract)
                .ThenInclude(x => x.Job)
                .Include(x => x.Holds)
                .ThenInclude(x => x.RelatedContract)
                .ThenInclude(x => x.Driver)
                .ThenInclude(x => x.User)
                .Where(x => x.UserId == query.User.Id)
                .Select(x => new BalanceDto
                {
                    Value = x.Balance,
                    Availalble = x.AvailableBalance,
                    Holds = x.Holds.Select(y => new BalanceHoldDto
                    {
                        Value = y.Value,
                        JobTittle = y.RelatedContract.Job.Tittle,
                        ContractState = y.RelatedContract.State,
                        Driver = y.RelatedContract.Driver.GetCustomerDriverDto()
                    })
                    .ToList()
                })
                .FirstOrDefaultAsync();

            return dto;
        }

        private static object pay_lock = new object();

        public void PaymentLock(Action payments)
        {
            lock (pay_lock)
            {
                payments.Invoke();
            }
        }

        public void PayFee(Guid fromUserId, Guid contractId, bool save)
        {
            var adminId = ctx.Roles
                .Include(x => x.Users)
                .Where(x => x.Name == Roles.Admin)
                .Select(x => x.Users.First().Id)
                .First();

            var fee = ctx.Contracts
                .Where(x => x.Id == contractId)
                .Select(x => x.ServiceFee)
                .First();

            Pay(fromUserId, adminId, fee, FinanceOperation.Fee, contractId, save);
        }

        public void Pay(Guid fromUserId, Guid toUserId, decimal value, string operation, Guid contractId, bool save)
        {
            var fromUserFinance = ctx.Finances
                    .Where(x => x.UserId == fromUserId)
                    .FirstOrDefault();
            var fromUserBalanceAfter = fromUserFinance.Balance - value;
            var fromUserAvailableBalanceAfter = fromUserFinance.AvailableBalance - value;

            var toUserFinance = ctx.Finances
                .Where(x => x.UserId == toUserId)
                .FirstOrDefault();
            var toUserBalanceAfter = toUserFinance.Balance + value;
            var toUserAvailableBalanceAfter = toUserFinance.AvailableBalance + value;

            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                Operation = operation,
                OperationValue = value,
                FromUserBalanceBefore = fromUserFinance.Balance,
                FromUserBalanceAfter = fromUserBalanceAfter,
                FromUserId = fromUserId,
                ToUserBalanceBefore = toUserFinance.Balance,
                ToUserBalanceAfter = toUserBalanceAfter,
                ToUserId = toUserId,
                RelatedContractId = contractId
            };
            fromUserFinance.Balance = fromUserBalanceAfter;
            fromUserFinance.AvailableBalance = fromUserAvailableBalanceAfter;
            toUserFinance.Balance = toUserBalanceAfter;
            toUserFinance.AvailableBalance = toUserAvailableBalanceAfter;

            ctx.Payments.Add(payment);

            if (save)
            {
                ctx.SaveChanges();
            }
        }

        public void AddHold(Guid userId, decimal value, Guid contractId, bool save)
        {
            var finance = ctx.Finances
                .Where(x => x.UserId == userId)
                .FirstOrDefault();

            var hold = new BalanceHold
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                FinanceId = finance.Id,
                Value = value,
                RelatedContractId = contractId
            };

            ctx.BalanceHolds.AddAsync(hold);

            finance.AvailableBalance = finance.AvailableBalance - value;

            if (save)
            {
                ctx.SaveChanges();
            }
        }

        public decimal RemoveHold(Guid userId, Guid contractId, bool save)
        {
            var finance = ctx.Finances
                .Include(x => x.Holds)
                .Where(x => x.UserId == userId)
                .FirstOrDefault();

            var hold = finance.Holds
                .Where(x => x.RelatedContractId == contractId)
                .FirstOrDefault();

            ctx.BalanceHolds.Remove(hold);

            finance.AvailableBalance = finance.AvailableBalance + hold.Value;

            if (save)
            {
                ctx.SaveChanges();
            }

            return hold.Value;
        }
    }
}
