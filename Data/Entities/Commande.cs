using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularNetCoreSample.Data.Entities
{
    public class Commande
    {
        public int Id { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public Client client { get; set; }
    }
}
