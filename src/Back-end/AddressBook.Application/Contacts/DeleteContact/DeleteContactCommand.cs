using MediatR;

namespace AddressBook.Application.Contacts.DeleteContact
{
    public class DeleteContactCommand(int id) : IRequest<Unit>
    {
        public int Id { get; set; } = id;
    }
}
