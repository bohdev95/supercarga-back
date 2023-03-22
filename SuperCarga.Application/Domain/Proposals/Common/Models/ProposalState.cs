using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Proposals.Common.Models
{
    public static class ProposalState
    {
        public const string Pending = "Pending";
        public const string Accepted = "Accepted";
        public const string Hired = "Hired";
        public const string Canceled = "Canceled";
        public const string Closed = "Closed";

        public static List<string> Active = new List<string> { Pending, Accepted, Hired };

        public static List<string> Archived = new List<string> { Canceled, Closed };
    }
}
