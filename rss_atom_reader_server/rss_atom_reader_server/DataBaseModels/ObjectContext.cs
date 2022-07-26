using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using rss_atom_reader_server.Models;

namespace rss_atom_reader_server.DataBaseModels
{
    public class ObjectContext
    {
        public IConfiguration Configuration { get; }
        private IMongoDatabase _database = null;

        public ObjectContext(IOptions<Settings> settings)
        {
            Configuration = settings.Value.iConfigurationRoot;
            settings.Value.ConnectionString = Configuration.GetSection("MongoConection:ConnectionString").Value;
            settings.Value.Database = Configuration.GetSection("MongoConection:Database").Value;

            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
            {
                _database = client.GetDatabase(settings.Value.Database);
            }
        }

        public IMongoCollection<NewsFeed> NewsFeeds
        {
            get { return _database.GetCollection<NewsFeed>("News"); }
        }
    }
}
