using System.Threading.Tasks;
using MongoDB.Bson;

namespace rss_atom_reader_server.IRepository
{
    public interface IUserService
    {
        Task<ObjectId> GetAccountAsync(ObjectId userId);
        Task<ObjectId> LoginAsync(string email, string password);
    }
}