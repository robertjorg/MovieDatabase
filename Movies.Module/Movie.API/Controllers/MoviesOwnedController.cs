using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Movie.API.Controllers
{
    using System.Data.Entity;

    using Movie.API.Models;
    using Movie.Classes;
    using Movie.DataModel;

    [Route("MovieKeep/Users({userId})/Movies")]
    public class MoviesOwnedController : ApiController
    {
        private IMovieRepository movieRepo;

        private ModelFactory modelFactory;

        public MoviesOwnedController(IMovieRepository movieRepo)
        {
            this.movieRepo = movieRepo;
            this.modelFactory = new ModelFactory();
        }

        [Route("MovieKeep/Users({userId})/Movies")]
        public IEnumerable<MoviesOwnedModel> Get(int userId)
        {
            var results = this.movieRepo.GetMoviesForUser(userId).ToList().Select(m => this.modelFactory.Create(m));

            return results;
        }
    }
}
