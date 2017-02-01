using Movie.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Movie.API.Controllers
{
    using Movie.API.Models;
    using Movie.Classes;

    [Route("Movies/Users")]
    public class UsersController : ApiController
    {
        private IMovieRepository movieRepo;

        private ModelFactory modelFactory;

        public UsersController(IMovieRepository movieRepo)
        {
            this.movieRepo = movieRepo;
            this.modelFactory = new ModelFactory();
        }

        public IEnumerable<UserModel> Get()
        {
            var results = this.movieRepo.GetAllUsers().ToList().Select(u => this.modelFactory.Create(u));

            return results;
        }

        [Route("Movies/Users({id})")]
        public UserModel Get(int id)
        {
            return this.modelFactory.Create(this.movieRepo.GetSingleUser(id));
        }
    }
}
