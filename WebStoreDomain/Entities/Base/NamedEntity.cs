using WebStoreDomain.Entities.Interface;

namespace WebStoreDomain.Entities.Base
{
    public abstract class NamedEntity : Entity, INamedEntity
    { 
        public string Name { get; set; }
    }
}
