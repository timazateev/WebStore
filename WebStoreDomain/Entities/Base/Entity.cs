using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStoreDomain.Entities.Interface;

namespace WebStoreDomain.Entities.Base
{
    public abstract class Entity : IEntity
    {
        public int id { get; set; }
    }
}
