using AngularNetCoreSample.Data.Entities;
using AngularNotCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AngularNetCoreSample.Data
{
    internal class ClientRepository : IClientRepository
    {
        private readonly IConfiguration _appSettings;

        private ClientDBContext _context;
        public ClientRepository(ClientDBContext context, IConfiguration config)
        {
            _context = context;
            _appSettings = config;
        }

        #region Client
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public void AddClient(AngularNetCoreSample.Models.Client clientModel) 
        {
            Client entity = new Client();
            entity.Address = clientModel?.Address;
            entity.City = clientModel?.City;
            entity.Email = clientModel?.Email;
            entity.FirstName = clientModel?.FirstName;
            entity.LastName = clientModel?.LastName;
            entity.Gender = clientModel?.Gender;


            _context.Clients.Add(entity);
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
        #endregion

        #region users
        public void AddUser(AngularNetCoreSample.Models.User userModel)
        {
            User entity = new User();
            entity.password = userModel?.password;
            entity.email = userModel?.email;
            entity.firstName = userModel?.firstName;
            entity.lastName = userModel?.lastName;


            _context.Users.Add(entity);
        }
        public User Authenticate(string email, string password)
        {
            var user = _context.Users.SingleOrDefault(x => x.email == email && x.password == password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var appSettingSec = _appSettings.GetSection("AppSetting:key");
            var key = Encoding.ASCII.GetBytes(appSettingSec.Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1), //expires in 1 day
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            user.password = null;

            return user;
        }
        public IEnumerable<User> GetAll()
        {
            // return users without passwords
            return _context.Users.OrderBy(c => c.email)
                      .ToList();
        }
        #endregion
    }
}
