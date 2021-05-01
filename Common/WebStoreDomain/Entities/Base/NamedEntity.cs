using System.ComponentModel.DataAnnotations;
using WebStoreDomain.Entities.Interface;

namespace WebStoreDomain.Entities.Base
{
    public abstract class NamedEntity : Entity, INamedEntity
    { 
        [Required]
        public string Name { get; set; }
    }
}
