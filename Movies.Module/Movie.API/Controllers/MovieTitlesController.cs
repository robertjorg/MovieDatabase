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
        private IMovieRepository movieRepo;

        private ModelFactory modelFactory;

        public MovieTitlesController(IMovieRepository movieRepo)
        {
            this.movieRepo = movieRepo;
            this.modelFactory = new ModelFactory();
        }

        [Route("MovieKeep/MovieTitles")]
        public IEnumerable<MovieTitlesModel> Get()
        {
            return this.movieRepo.GetMovieTitles().ToList().Select(t => this.modelFactory.Create(t));
        }

        [Route("MovieKeep/Users{(userId)}/MoviesOwned{(moviesOwnedId)}/MovieTitles")]
        public IEnumerable<MovieTitlesModel> Get(int userId, int moviesOwnedId)
        {
            return this.movieRepo.GetMovieTitles().ToList().Select(t => this.modelFactory.Create(t));
        }

        [Route("MovieKeep/MovieTitles({id})")]
        public MovieTitlesModel Get(int id)
        {
            return this.modelFactory.Create(this.movieRepo.GetTitleForMovie(id));
        }

        [Route("MovieKeep/User{(userId)}/MoviesOwned{(moviesOwnedId)}/MovieTitles({id})")]
        public MovieTitlesModel Get(int userId, int moviesOwnedId, int id)
        {
            return this.modelFactory.Create(this.movieRepo.GetTitleForMovie(id));
        }
    }
}
