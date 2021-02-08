using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPlusSupport.API.Models
{
    public class Category
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}
