using AdressBook.Persistance.TrakingEntity.Interface;

namespace AdressBook.Persistance.TrakingEntity
{
    public class BaseEntity : ITrackEntity
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }
}
