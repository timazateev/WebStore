using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStoreDomain.Entities.Base;
using WebStoreDomain.Entities.Interface;

namespace WebStoreDomain.Entities
{
    [Table("Brands")]
    public class Brand : NamedEntity, IOrderedEntity
    {

        public int Order { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
