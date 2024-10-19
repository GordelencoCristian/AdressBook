using AddressBook.Application.Infrastructure.Pagination.BasedPagedList;
using AddressBook.Application.Infrastructure.Pagination.Parameter;

namespace AddressBook.Application.Infrastructure.Pagination.Model
{
    public class PaginatedModel<T>(BasePagedList<T> list)
    {
        public IEnumerable<T> Items { get; set; } = list;
        public PaginatedHeaderParameter PagedSummary { get; set; } = list.PagedSummary;
    }
}
