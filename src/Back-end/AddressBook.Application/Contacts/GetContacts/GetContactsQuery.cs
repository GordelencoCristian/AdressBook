using AddressBook.Application.Infrastructure.Pagination.Model;
using AddressBook.Application.Infrastructure.Pagination.Parameter;
using AddressBook.DataTrasnferObjects.Contacts;
using MediatR;

namespace AddressBook.Application.Contacts.GetContacts
{
    public class GetContactsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<ContactDto>>
    {
    }
}
