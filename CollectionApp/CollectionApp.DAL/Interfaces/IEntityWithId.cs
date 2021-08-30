namespace CollectionApp.DAL.Interfaces
{
    public interface IEntityWithId<T>
    {
        T Id { get; set; }
    }
}
