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
    using Movie.DataModel;

    public class MoviesOwnedController : ApiController
    {
        private IMovieRepository movieRepository;

        private ModelFactory modelFactory;

        public MoviesOwnedController(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
            this.modelFactory = new ModelFactory();
        }

        [Route("MovieKeep/Users({userId})/Movies")]
        public IEnumerable<MoviesOwnedModel> Get(int userId)
        {
            var results = this.movieRepository.GetMoviesForUser(userId).ToList().Select(m => this.modelFactory.Create(m));

            return results;
        }
    }
}
