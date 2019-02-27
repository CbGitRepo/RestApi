using AngularNetCoreSample.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularNetCoreSample.Data
{
    public class ClientDbInitializer
    {
        private ClientDBContext _ctx;
        public ClientDbInitializer(ClientDBContext ctx)
        {
            _ctx = ctx;
        }

        public async Task Seed()
        {
            if (!_ctx.Clients.Any())
            {
                // Add Data
                _ctx.AddRange(_sample);
                await _ctx.SaveChangesAsync();
            }
            
        }
        List<Client> _sample = new List<Client>
    {
      new Client()
      {
        FirstName = "Chaima",
        LastName = "boua",
        Address = "Tunis denden",
        Email="carpediamchaima@gmail.com",
        Gender="Female",
        Commandes = new List<Commande>
        {
         new  Commande ()
         {
Quantity=1,
Price=12,
             Products = new List<Product>
             {
                 new Product()
                 {
                     productName="Milk",
                     productCategory="diary",
                     productDescription="nutrition",
                     productPrice=12,
                     productWeight=1
                 
                 }
             }

         }
        
        }
      }
    };

    }
}
