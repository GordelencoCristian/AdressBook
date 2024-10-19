using AddressBook.Application.Infrastructure.Pagination.Model;
using AddressBook.Application.Infrastructure.Pagination.Parameter;
using System.Linq.Expressions;

namespace AddressBook.Application.Infrastructure.Pagination.Services
{
    public interface IPaginationService
    {
        Task<PaginatedModel<TDestination>> MapAndPaginateModelAsync<TSource, TDestination>(IQueryable<TSource> list, PaginatedQueryParameter query, Expression<Func<TSource, string>>[]? membersToSearch = null);
    }
}
