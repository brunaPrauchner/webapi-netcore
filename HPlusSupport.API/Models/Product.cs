using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HPlusSupport.API.Models
{
    public class Product
    {
        public int Id { get; set; }
        public String Sku { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }

        public int CategoryId { get; set; }
        [JsonIgnore]
        public virtual Category Category { get; set; }
    }
}
