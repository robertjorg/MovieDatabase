using Movie.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Movie.API.Controllers
{
    using Movie.Classes;

    [Route("Movies/Users")]
    public class UsersController : ApiController
    {
        public IEnumerable<User> Get()
        {
            var repo = new MovieRepository(new MovieContext());

            var results = repo.GetAllUsers().ToList();

            return results;
        }

        [Route("Movies/Users({id})")]
        public User Get(int id)
        {
            var repo = new MovieRepository(new MovieContext());

            var results = repo.GetSingleUser(id);

            return results;
        }
    }
}
