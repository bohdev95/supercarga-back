using MediatR;
using SuperCarga.Application.Domain.Finances.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Finances.Queries.GetBalance
{
    public class GetBalanceQueryHandler : IRequestHandler<GetBalanceQuery, GetBalanceQueryResponse>
    {
        private readonly IFinancesService financesService; 

        public GetBalanceQueryHandler(IFinancesService financesService)
        {
            this.financesService = financesService;
        }

        public async Task<GetBalanceQueryResponse> Handle(GetBalanceQuery request, CancellationToken cancellationToken)
        {
            var response = new GetBalanceQueryResponse();

            response.Balance = await financesService.GetBalance(request);

            return response;
        }
    }
}
