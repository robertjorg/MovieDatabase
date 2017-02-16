using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Movie.API.Controllers
{
    using System.Web.Http.Routing;

    using Movie.API.Models;
    using Movie.API.Validations;
    using Movie.Classes;
    using Movie.DataModel;

    [Route("MovieKeep/MovieTitles")]
    public class MovieTitlesController : BaseApiController
    {
        private const int PageSize = 50;

        public MovieTitlesController(IMovieRepository movieRepository) : base(movieRepository)
        {
        }

        [Route("MovieKeep/MovieTitles")]
        public object Get(double page = 0)
        {
            var pages = Convert.ToInt32(page);
            IQueryable<MovieTitles> query = this.movieRepo.GetMovieTitles();

            var basequery = query.OrderBy(t => t.MovieTitle);

            var totalCount = basequery.Count();
            var totalPages = Math.Ceiling((double)totalCount / PageSize);

            var helper = new UrlHelper(this.Request);
            var prevUrl = page > 0 ? helper.Link("MovieTitles", new { page = page - 1 }) : string.Empty;
            var nextUrl = page < totalPages - 1 ? helper.Link("MovieTitles", new { page = page + 1 }) : string.Empty;

            var result =
                basequery.Skip(PageSize * pages).Take(PageSize).ToList().Select(t => this.modelFactory.Create(t));

            return
                new
                {
                    Results = result,
                    TotalCount = totalCount,
                    TotalPages = totalPages,
                    PrevPageUrl = prevUrl,
                    NextPageUrl = nextUrl
                };
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

        [HttpPost]
        public HttpResponseMessage Post([FromBody]MovieTitlesModel model)
        {
            try
            {
                var entity = this.modelFactory.Parse(model);

                if (entity == null)
                {
                    return this.Request.CreateErrorResponse(
                        HttpStatusCode.BadRequest,
                        "Bad formed JSON request.");
                }

                var titleCheck = this.Validate.CheckMovieTitles(entity.MovieTitle);

                if (titleCheck)
                {
                    if (this.movieRepo.Add(entity))
                    {
                        if (this.movieRepo.SaveAll())
                        {
                            this.movieRepo.GetTitleForMovie(entity.Id);
                            return this.Request.CreateResponse(HttpStatusCode.Created, this.modelFactory.Create(entity));
                        }
                    }
                }

                return this.Request.CreateErrorResponse(HttpStatusCode.Conflict, "Movie title already exists");
            }
            catch (Exception ex)
            {
                return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpDelete]
        [Route("MovieKeep/MovieTitles({id})")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                if (this.movieRepo.GetMovieTitles().ToList().All(mi => mi.Id != id))
                {
                    return this.Request.CreateResponse(HttpStatusCode.NotFound);
                }

                if (this.movieRepo.DeleteMovieTitle(id) && this.movieRepo.SaveAll())
                {
                    return this.Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return this.Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
            catch (Exception ex)
            {
                return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPatch]
        [Route("MovieKeep/MovieTitles({id})")]
        public HttpResponseMessage Patch(int id, [FromBody] MovieTitlesModel model)
        {
            try
            {
                var entity = this.movieRepo.GetTitleForMovie(id);
                if (entity == null)
                {
                    return this.Request.CreateResponse(HttpStatusCode.NotFound);
                }

                var parsedMovieValue = this.modelFactory.ParsePatch(model, id);
                if (parsedMovieValue == null)
                {
                    return this.Request.CreateResponse(HttpStatusCode.BadRequest);
                }

                if (!this.Validate.CheckMovieTitlesPatch(model.MovieTitle, "patch"))
                {
                    return this.Request.CreateErrorResponse(
                        HttpStatusCode.BadRequest,
                        "A movie title with a differnt ID already has contains that movie title");
                }

                if (this.movieRepo.UpdateMovieTitle(parsedMovieValue) && this.movieRepo.SaveAll())
                {
                    return this.Request.CreateResponse(HttpStatusCode.OK);
                }

                return this.Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
