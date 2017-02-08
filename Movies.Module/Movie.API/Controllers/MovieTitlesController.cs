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
    public class MovieTitlesController : BaseApiController
    {
        public MovieTitlesController(IMovieRepository movieRepository) : base(movieRepository)
        {
        }

        [Route("MovieKeep/MovieTitles")]
        public IEnumerable<MovieTitlesModel> Get()
        {
            return this.movieRepo.GetMovieTitles().ToList().Select(t => this.modelFactory.Create(t));
        }

        [Route("MovieKeep/Users({userId})/MoviesOwned({moviesOwnedId})/MovieTitles")]
        public IEnumerable<MovieTitlesModel> Get(int userId, int moviesOwnedId)
        {
            return this.movieRepo.GetMovieTitles().ToList().Select(t => this.modelFactory.Create(t));
        }

        [Route("MovieKeep/MovieTitles({id})")]
        public MovieTitlesModel Get(int id)
        {
            return this.modelFactory.Create(this.movieRepo.GetTitleForMovie(id));
        }

        [Route("MovieKeep/User({userId})/MoviesOwned({moviesOwnedId})/MovieTitles({id})")]
        public MovieTitlesModel Get(int userId, int moviesOwnedId, int id)
        {
            return this.modelFactory.Create(this.movieRepo.GetTitleForMovie(id));
        }

        public object Post([FromBody]MovieTitlesModel model)
        {
            try
            {
                var entity = this.modelFactory.Parse(model);

                if (entity == null)
                {
                    return this.Request.CreateErrorResponse(
                        HttpStatusCode.BadRequest,
                        "Bad formed JSON request. Movie Title is required");
                }

                if (this.movieRepo.Add(entity))
                {
                    this.movieRepo.GetTitleForMovie(entity.Id);
                }
                return this.Request.CreateResponse(HttpStatusCode.Created, this.modelFactory.Create(entity));
            }
            catch (Exception ex)
            {
                return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
    }
}
