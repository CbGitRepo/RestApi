using AngularNetCoreSample.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace AngularNetCoreSample.Data
{
    public class ClientRepository : IClientRepository
    {
        private ClientDBContext _context;
        public ClientRepository(ClientDBContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
        public async Task<bool> SaveAllAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
        public IEnumerable<Client> GetAllCLient()
        {
            return _context.Clients
                      .OrderBy(c => c.FirstName)
                      .ToList();
        }
       public Client GetClient(int id)
        {
            return _context.Clients.Where(c=>c.Id==id).FirstOrDefault();
        }
        public List<Commande> GetCommandesForClient(int id)
        {
            return _context.Commandes
              .Where(c => c.client.Id == id).ToList<Commande>();
              
        }
        public List<Product> GetProductsForCommmand(int idCLient, int idCommand)
        {
            return _context.Products
              .Where(c => c.commande.Id == idCommand).ToList<Product>();
            
        }

    }
}
