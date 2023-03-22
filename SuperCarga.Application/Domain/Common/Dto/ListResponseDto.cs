namespace SuperCarga.Application.Domain.Common.Dto
{
    public class ListResponseDto<T>
    {
        public List<T> Data { get; set; }

        public int TotalCount { get; set; }

        public int Pages { get; set; }

        public int PageSize { get; set; }

        public int CurrentPage { get; set; }
    }
}
