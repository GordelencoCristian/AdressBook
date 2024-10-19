using AdressBook.Persistance.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.Application.Contacts.DeleteContact
{
    public class DeleteContactCommandHandler(AppDbContext context) : IRequestHandler<DeleteContactCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
        {
            var toRemove = await context.Contacts.FirstAsync(a => a.Id == request.Id, cancellationToken: cancellationToken);

            context.Contacts.Remove(toRemove);
            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
