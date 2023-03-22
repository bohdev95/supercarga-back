using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Contracts.Common.Abstraction
{
    public interface IContractsService
    {
        bool ContractExists(Guid id);

        string GetState(Guid id);
    }
}
