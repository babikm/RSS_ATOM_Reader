using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace rss_atom_reader_server.Controllers
{
    [Route("[controller]")]
    public class ApiBaseController : Controller
    {
        protected ObjectId UserId => User?.Identity?.IsAuthenticated == true ?
            ObjectId.Parse(User.Identity.Name) :
            ObjectId.Empty;
    }
}