using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularNetCoreSample.Data;
using AngularNetCoreSample.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AngularNetCoreSample.Controllers
{
    public class UserController : Controller
    {
        IClientRepository _repo;
        ILogger _logger;
        IConfiguration _configuration;
        public UserController(IClientRepository repo, ILoggerFactory logger,IConfiguration config)
        {
            _repo = repo;
            _logger = logger.CreateLogger(nameof(ClientController));
            _configuration = config;
        }
        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="objUser"></param>
        /// <returns></returns>
        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> InsertUser([FromBody]User objUser)
        {
            var identity = new IdentityUser
            {
                Email = objUser.email,
                UserName = objUser.email,
                SecurityStamp = Guid.NewGuid().ToString()

            };
        _repo.Add(objUser);

            if (await _repo.SaveAllAsync())
            {
                return Ok(new { UserName = objUser.email });
            }
            return BadRequest();
}
    }
}