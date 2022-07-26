using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using rss_atom_reader_server.IRepository;
using rss_atom_reader_server.Models;

namespace rss_atom_reader_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsFeedController : ControllerBase
    {
        private readonly INewsFeedRepository _newsFeedRepository;

        public NewsFeedController(INewsFeedRepository newsFeedRepository)
        {
            _newsFeedRepository = newsFeedRepository;
        }
        // GET api/newsfeed
        [HttpGet]
        public Task<string> Get()
        {
            return this.GetFeed();
        }

        private async Task<string> GetFeed()
        {
            var feed = await _newsFeedRepository.Get();
            return JsonConvert.SerializeObject(feed);
        }

        // POST api/[controller]
        [HttpPost]
        public async void Post()
        {
            await _newsFeedRepository.Add();
        }

        // DELETE api/[controller]
        [HttpDelete]
        public async Task<string> Delete()
        {
            await _newsFeedRepository.RemoveAll();
            return "";
        }
    }
}
