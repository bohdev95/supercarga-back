using Microsoft.EntityFrameworkCore;
using SuperCarga.Application.Domain.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Domain.Domain.Common
{
    public static class PaginationExtensions
    {
        public static async Task<ListResponseDto<T>> Paginate<T>(this IQueryable<T> query, ListRequestDto listRequest)
        {
            var response = new ListResponseDto<T>();

            var page = listRequest.Page == null || listRequest.Page.Value < 1 ? 0 : listRequest.Page.Value - 1;
            var pageSize = listRequest.PageSize == null || listRequest.PageSize.Value < 1 ? 10 : listRequest.PageSize.Value;
            var skip = page * pageSize;

            var list = await query
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            response.Data = list;
            response.TotalCount = query.Count();
            response.Pages = GetPagesCount();
            response.PageSize = pageSize;
            response.CurrentPage = page + 1;

            int GetPagesCount()
            {
                var res = response.TotalCount / pageSize;
                var mod = response.TotalCount % pageSize;
                if (mod > 0)
                    res++;
                return res;
            }

            return response;
        }
    }
}
