using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularNetCoreSample.Data.Entities
{
    public class Product
    {
        public int ID { get; set; }
        public string productCategory { get; set; }
        public string productName { get; set; }
        public string productDescription { get; set; }
        public int productPrice { get; set; }
        public int productWeight { get; set; }
        public Commande commande { get; set; }
    }
}
