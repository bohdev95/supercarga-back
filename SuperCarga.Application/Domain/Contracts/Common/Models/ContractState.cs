using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Contracts.Common.Models
{
    public static class ContractState
    {
        public const string Created = "Created";
        public const string Started = "Started";
        public const string InProgress = "In progress";
        public const string Delivered = "Delivered";
        public const string DeliveredConfirmed = "Delivered confirmed";
        public const string Canceled = "Canceled";
        public const string Closed = "Closed";

        public static List<string> Active = new List<string> { Started, InProgress, Delivered, DeliveredConfirmed };

        public static List<string> Finished = new List<string> { Canceled, Closed };
    }
}
