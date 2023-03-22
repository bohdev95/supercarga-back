namespace SuperCarga.Application.Domain.Jobs.Customers.Queries.List.Common.Dto
{
    public class CustomerJobListItemDto
    {
        public Guid Id { get; set; }

        public DateTime Created { get; set; }

        public string Tittle { get; set; }

        public int Proposals { get; set; }

        public int NewProposals { get; set; }

        public int Hired { get; set; }
    }
}
