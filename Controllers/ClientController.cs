using AngularNetCoreSample.Data;
using AngularNetCoreSample.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularNetCoreSample.Controllers
{
    /// <summary>
    /// Client controller will allow you to extract all client information
    /// </summary>
    [Route("api/clients")]
    [EnableCors("AllowAllOrigins")]
    public class ClientController : Controller
    {
        IClientRepository _repo;
        ILogger _logger;
         public ClientController (IClientRepository repo , ILoggerFactory logger)
        {
            _repo = repo;
            _logger = logger.CreateLogger(nameof(ClientController));
        }
        /// <summary>
        /// This will return all registered clients
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetClients()
        {
            try
            {
                var clients =  _repo.GetAllCLient();
                return Ok(clients);
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message);
                return BadRequest("Sorry!!");
            }
        }
        //[HttpGet("{id}")]

        public IActionResult GetClient(int id)
        {
            try
            {
                var client = _repo.GetClient(id);
                return Ok(client);
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message);
                return BadRequest($"Sorry! No commandes was found for the ID {id}");
            }
        }
        /// <summary>
        /// This will return the list of orders for a specific client
        /// </summary>
        /// <param name="id"> the Id of the client</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetCommandesForClient(int id)
        {
            try
            {
                var commandes = _repo.GetCommandesForClient(id);
                return Ok(commandes);
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message);
                return BadRequest($"Sorry! No commandes was found for the ID {id}");
            }
        }
        /// <summary>
        /// This will return the product of a specific order of a client
        /// </summary>
        /// <param name="idclient">The client Id</param>
        /// <param name="idcommand">The order Id</param>
        /// <returns></returns>
        [HttpGet("{idclient}/{idcommand}")]
        
        public IActionResult GetProducts(int idclient, int idcommand)
        {
            try
            {
                var products = _repo.GetProductsForCommmand(idclient,idcommand);
                return Ok(products);
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message);
                return BadRequest($"Sorry! No products was found for the command ID {idcommand}");
            }
        }
        /// <summary>
        /// This will add a client to the list of registered clients
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> add([FromBody] Client item)
        {
            _repo.Add(item);

            if (await _repo.SaveAllAsync())
            {
                var varUrl = Url.Link("getClient", new { id = item.Id });
                return Created(varUrl, item);
            }
            return BadRequest();
        }
        /// <summary>
        /// This will update the client information
        /// </summary>
        /// <param name="id">The client Id</param>
        /// <param name="newItem"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] Client newItem)
        {
            var oldItem = _repo.GetClient(id);

            if (oldItem != null)
                oldItem.FirstName = newItem.FirstName ?? oldItem.FirstName;
            else return NotFound($"Could not found item {id}");

            if (await _repo.SaveAllAsync())
            {
                return Ok(oldItem);
            }
            return BadRequest();
        }
        /// <summary>
        /// This will remove the client from the list of registered clients
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var oldItem = _repo.GetClient(id);

            if (oldItem != null)
                _repo.Delete(oldItem);
            else return NotFound($"Could not found item {id}");

            if (await _repo.SaveAllAsync())
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
