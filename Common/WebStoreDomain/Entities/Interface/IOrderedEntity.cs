namespace WebStore.Domain.Entities.Interface
{
    interface IOrderedEntity : IEntity
    {
        int Order { get; set; }
    }
}
