using AdressBook.Persistance.Contexts;
using AdressBook.Persistance.TrakingEntity;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;


namespace AddressBook.Application.Infrastructure.Validators
{
    public class ItemMustExistValidator<TEntity> : AbstractValidator<int> where TEntity : BaseEntity
    {
        private readonly TrackableDbContext _context;

        public ItemMustExistValidator(TrackableDbContext context, string errorCode, string errorMessage)
        {
            _context = context;

            RuleFor(x => x).Custom((id, c) => ExistentRecord(id, errorCode, errorMessage, c));
        }

        private void ExistentRecord(int id, string errorCode, string errorMessage, ValidationContext<int> context)
        {
            DbSet<TEntity> collections = _context.Set<TEntity>();

            var existent = collections.Any(x => x.Id == id);

            if (!existent)
            {
                context.AddFailure(new ValidationFailure($"{context.DisplayName}", errorMessage)
                {
                    ErrorCode = errorCode
                });
            }
        }
    }
}
