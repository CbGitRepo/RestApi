using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularNetCoreSample.Models
{
    public class Commande
    {
        public int Id { get; set; }
        public ICollection<Product>  Products { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public Client client { get; set; }

    }
}
