using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WebStoreDomain.Entities.Base;
using WebStoreDomain.Entities.Interface;

namespace WebStoreDomain.Entities
{
    [Table("Sections")]
    public class Section : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }

        public int? ParentId { get; set; }

        [ForeignKey(nameof(ParentId))]
        public Section Parent { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
