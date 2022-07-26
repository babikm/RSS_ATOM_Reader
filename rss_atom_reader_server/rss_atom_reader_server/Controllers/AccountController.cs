using System;
using System.Threading.Tasks;
using rss_atom_reader_server.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using rss_atom_reader_server.Models;
using MongoDB.Bson;

namespace rss_atom_reader_server.Controllers

{
    public class AccountController : ApiBaseController
    {
        private IUserService _userService;


        public AccountController(IUserService userService)
        {
            _userService = userService;

        }

        [HttpGet("id")]
        [Authorize]
        public async Task<IActionResult> Get()
            => Json(await _userService.GetAccountAsync(UserId));


        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody]Login command)
            => Json(await _userService.LoginAsync(command.Email, command.Password));
    }
}