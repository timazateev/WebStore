namespace WebStoreDomain.Entities.Interface
{
    interface IOrderedEntity : IEntity
    {
        int Order { get; set; }
    }
}
