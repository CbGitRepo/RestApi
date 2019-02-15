using AngularNetCoreSample.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularNetCoreSample.Data
{
    public interface IClientRepository
    {
        // Basic DB Operations
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAllAsync();

        
        IEnumerable<Client> GetAllCLient();
        List<Commande> GetCommandesForClient(int id);
        List<Product>GetProductsForCommmand(int idCLient,int idCommand);
        Client GetClient(int id);

    }
}
