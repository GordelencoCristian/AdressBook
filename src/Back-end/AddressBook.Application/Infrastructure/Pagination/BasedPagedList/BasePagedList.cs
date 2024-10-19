using AddressBook.Application.Infrastructure.Pagination.Parameter;
using System.Linq.Expressions;

namespace AddressBook.Application.Infrastructure.Pagination.BasedPagedList
{
    public class PaginatedList<T>(List<T> items, int count, int pageNumber, int pageSize) : BasePagedList<T>(items, count, pageNumber, pageSize);

    public abstract class BasePagedList<T> : List<T>
    {
        protected BasePagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            PagedSummary = new PaginatedHeaderParameter()
            {
                TotalCount = count,
                PageSize = pageSize,
                CurrentPage = pageNumber
            };

            AddRange(items);
        }

        public PaginatedHeaderParameter PagedSummary { get; set; }
    }

}
