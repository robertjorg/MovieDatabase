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
    using Movie.API.Services;
    using Movie.Classes;
    using Movie.DataModel;

    [Route("MovieKeep/Users({userId})/Movies")]
    public class MoviesOwnedController : BaseApiController
    {
        private IMovieIdentityService identityService;

        public MoviesOwnedController(IMovieRepository movieRepository, IMovieIdentityService identityService) : base(movieRepository)
        {
            this.identityService = identityService;
        }

        [Route("MovieKeep/Users({userId})/MoviesOwned")]
        public IEnumerable<MoviesOwnedModel> Get(int userId)
        {
            var username = this.identityService.CurrentUser; // How am I going to make you sign in? Is that going to be on the UX. If so, I may not need this piece.
            var results = this.movieRepo.GetMoviesForUser(userId).ToList().OrderBy(m => m.MovieTitles.MovieTitle).Select(m => this.modelFactory.Create(m));

            return results;
        }

        [Route("MovieKeep/Users({userId})/MoviesOwned({id})")]
        public HttpResponseMessage Get(int userId, int id)
        {
            var result = this.movieRepo.GetSingleMovieOwned(userId, id);

            if (result == null)
            {
                return this.Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return this.Request.CreateResponse(HttpStatusCode.OK, this.modelFactory.Create(this.movieRepo.GetSingleMovieOwned(userId, id)));
        }

        [Route("MovieKeep/Users({userId})/MoviesOwned")]
        public HttpResponseMessage Post(int userId, [FromBody] MoviesOwnedModel moviesOwned)
        {
            try
            {
                var entity = this.modelFactory.Parse(moviesOwned, userId);

                if (entity == null)
                {
                    return this.Request.CreateErrorResponse(
                        HttpStatusCode.BadRequest,
                        "Bad formed JSON request.");
                }

                var alreadyOwned = this.Validate.CheckMoviesOwned(userId, entity.MovieTitlesId);

                if (alreadyOwned != string.Empty)
                {
                    return this.Request.CreateErrorResponse(HttpStatusCode.Conflict, alreadyOwned);
                }

                if (this.movieRepo.Add(entity) && this.movieRepo.SaveAll())
                {
                    return this.Request.CreateResponse(HttpStatusCode.NoContent, this.modelFactory.Create(entity));
                }

                return this.Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception e)
            {
                return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        [Route("MovieKeep/Users({userId})/MoviesOwned({id})")]
        public HttpResponseMessage Delete(int userId, int id)
        {
            try
            {
                var entity = this.movieRepo.GetSingleMovieOwned(userId, id);
                if (entity == null)
                {
                    return this.Request.CreateResponse(HttpStatusCode.NotFound);
                }

                if (this.movieRepo.DeleteMovieOwned(userId, id) && this.movieRepo.SaveAll())
                {
                    return this.Request.CreateResponse(HttpStatusCode.OK);
                }

                return this.Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception e)
            {
                return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        [Route("MovieKeep/Users({userId})/MoviesOwned({id})")]
        public HttpResponseMessage Patch(int userId, int id, [FromBody] MoviesOwnedModel model)
        {
            try
            {
                var entity = this.movieRepo.GetSingleMovieOwned(userId, id);
                if (entity == null)
                {
                    return this.Request.CreateResponse(HttpStatusCode.NotFound);
                }

                var parsedMovieOwnedValue = this.modelFactory.ParsePatch(model, userId, id);
                if (parsedMovieOwnedValue == null)
                {
                    return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Only storage type may be patched on movies owned.");
                }

                if (this.movieRepo.SaveAll())
                {
                    return this.Request.CreateResponse(HttpStatusCode.OK, this.modelFactory.Create(parsedMovieOwnedValue));
                }

                return this.Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception e)
            {
                return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }
    }
}
