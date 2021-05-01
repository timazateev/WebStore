namespace WebStore.Domain.Entities.Interface
{
    interface INamedEntity : IEntity
    {
        string Name { get; set; }
    }
}
