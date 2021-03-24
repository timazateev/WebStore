namespace WebStoreDomain.Entities.Interface
{
    interface INamedEntity : IEntity
    {
        string Name { get; set; }
    }
}
