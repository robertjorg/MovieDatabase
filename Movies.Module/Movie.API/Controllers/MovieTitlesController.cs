using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Movie.API.Controllers
{
    using Movie.API.Models;
    using Movie.DataModel;

    [Route("MovieKeep/MovieTitles")]
    public class MovieTitlesController : ApiController
    {
        private IMovieRepository movieRepository;

        private ModelFactory modelFactory;

        public MovieTitlesController(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
            this.modelFactory = new ModelFactory();
        }

        [Route("MovieKeep/User({userId})/MoviesOwned/MovieTitles({id})")]
        public MovieTitlesModel Get(int moviesTitleId)
        {
            var results =
                this.modelFactory.Create(this.movieRepository.GetTitleForMovie(moviesTitleId));

            return results;
        }
    }
}
