using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Pagenation
{
    public class PaginatedResult<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public object Meta { get; set; }
        public int PageSize { get; set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
        public List<string> Messages { get; set; } = new();
        public List<T> Data { get; set; }
        public bool Succeeded { get; set; }
        public PaginatedResult(List<T> data)
        {
            Data = data;
        }
        public PaginatedResult(bool succeeded, List<T> data = default, List<string> messages = null, int count = 0, int page = 1, int size = 1)
        {
            Data = data;
            CurrentPage = page;
            PageSize = size;
            Succeeded = succeeded;
            TotalPages = (int)Math.Ceiling(count / (double)PageSize);
            TotalCount = count;

        }
        public static PaginatedResult<T> Success(List<T> data, int count, int PageNumber, int PageSize)
        {
            return new(true, data, null, count, PageNumber, PageSize);
        }
    }
}
