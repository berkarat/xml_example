using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xml_example_berkarat.com
{
    public class Messages
    {

    }
    public class Product
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public ProductDetails[] ProductDetay { get; set; }
    }
    public class ProductDetails
    {
        public string ProductSerialNumber { get; set; }
        public string ProductName { get; set; }

    }
}
