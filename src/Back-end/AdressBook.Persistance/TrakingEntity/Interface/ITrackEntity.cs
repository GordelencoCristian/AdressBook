namespace AdressBook.Persistance.TrakingEntity.Interface
{
    public interface ITrackEntity
    {
        int Id { get; set; }
        DateTime CreateDate { get; set; }
        DateTime? UpdateDate { get; set; }
        bool IsDeleted { set; get; }
        public DateTimeOffset? DeletedAt { get; set; }

        public void Undo()
        {
            IsDeleted = false;
            DeletedAt = null;
        }
    }
}
