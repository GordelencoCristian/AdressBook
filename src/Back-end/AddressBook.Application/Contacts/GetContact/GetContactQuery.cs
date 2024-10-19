using AddressBook.DataTrasnferObjects.Contacts;
using MediatR;

namespace AddressBook.Application.Contacts.GetContact
{
    public class GetContactQuery(int id) : IRequest<ContactDto>
    {
        public int Id { get; set; } = id;
    }
}
