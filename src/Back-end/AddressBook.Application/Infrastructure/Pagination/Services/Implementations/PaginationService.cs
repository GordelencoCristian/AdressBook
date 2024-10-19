using AddressBook.Application.Infrastructure.Pagination.BasedPagedList;
using AddressBook.Application.Infrastructure.Pagination.Model;
using AddressBook.Application.Infrastructure.Pagination.Parameter;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace AddressBook.Application.Infrastructure.Pagination.Services.Implementations
{
    public class PaginationService(IMapper mapper) : IPaginationService
    {
        public async Task<PaginatedModel<TDestination>> MapAndPaginateModelAsync<TSource, TDestination>(IQueryable<TSource> queryableList, PaginatedQueryParameter pagedQuery, Expression<Func<TSource, string>>[]? membersToSearch = null)
        {
            var paginatedList = await Create<TSource, TDestination>(queryableList, pagedQuery.Page, pagedQuery.ItemsPerPage);

            return new PaginatedModel<TDestination>(paginatedList);
        }

        public async Task<PaginatedList<TDestination>> Create<TSource, TDestination>(IQueryable<TSource> source, int pageNumber, int pageSize)
        {
            var count = source.CountAsync();
            var skipCount = (pageNumber - 1) * pageSize;

            await count;

            var items = source.Skip(skipCount).Take(pageSize).ToListAsync();
            var mappedItems = mapper.Map<List<TDestination>>(items.Result).ToList();

            return new PaginatedList<TDestination>(mappedItems, count.Result, pageNumber, pageSize);
        }
    }
}
