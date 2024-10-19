using AddressBook.Application.Infrastructure.Pagination.Model;
using AddressBook.Application.Infrastructure.Pagination.Services;
using AddressBook.DataTrasnferObjects.Contacts;
using AdressBook.Persistance.Contexts;
using AdressBook.Persistance.Entities;
using MediatR;

namespace AddressBook.Application.Contacts.GetContacts
{
    public class GetContactsQueryHandler(AppDbContext context, IPaginationService paginationService) : IRequestHandler<GetContactsQuery, PaginatedModel<ContactDto>>
    {
        public async Task<PaginatedModel<ContactDto>> Handle(GetContactsQuery request, CancellationToken cancellationToken)
        {
            var queryableList = context.Contacts.Select(contact => new Contact()
            {
                Id = contact.Id,
                Address = contact.Address,
                Email = contact.Email,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                PhoneNumber = contact.PhoneNumber,
            }); 

            var paginatedModel = await paginationService.MapAndPaginateModelAsync<Contact, ContactDto>(queryableList, request);
            return paginatedModel;
        }
    }
}
