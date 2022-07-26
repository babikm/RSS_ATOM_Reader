using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using rss_atom_reader_server.Models;

namespace rss_atom_reader_server.IRepository
{
    public interface INewsFeedRepository
    {
        Task<IEnumerable<NewsFeed>> Get();
        Task Add();
        Task<DeleteResult> RemoveAll();
    }
}
