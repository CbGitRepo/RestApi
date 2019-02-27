using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularNetCoreSample.Data;
using AngularNetCoreSample.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AngularNetCoreSample.Controllers
{
    [EnableCors("AllowAllOrigins")]
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
        public async Task<IActionResult> InsertUser([FromBody] User objUser)
        {
           if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _repo.AddUser(objUser);

            if (await _repo.SaveAllAsync())
            {
                return Ok(new { UserName = objUser.email });
            }
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Authenticate([FromBody]User userParam)
        {
            var user = _repo.Authenticate(userParam.email, userParam.password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _repo.GetAll();
            return Ok(users);
        }
    }
}