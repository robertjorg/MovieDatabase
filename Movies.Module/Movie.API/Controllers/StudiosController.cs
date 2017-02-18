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

    [Route("MovieKeep/Studios")]
    public class StudiosController : BaseApiController
    {
        public StudiosController(IMovieRepository movieRepository)
            : base(movieRepository)
        {
        }

        public IEnumerable<StudiosModel> Get()
        {
            return this.movieRepo.GetStudios().ToList().Select(s => this.modelFactory.Create(s));
        }

        [Route("MovieKeep/Studios({id})")]
        public StudiosModel Get(int id)
        {
            var result = this.modelFactory.Create(this.movieRepo.GetSingleStudio(id));

            return result;
        }

        public HttpResponseMessage Post([FromBody] StudiosModel studioModel)
        {
            try
            {
                var entity = this.modelFactory.Parse(studioModel);

                if (entity == null)
                {
                    return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad formed JSON request.");
                }

                var studioCheck = this.Validate.StudioNameCheck(entity.StudioName);

                if (studioCheck != string.Empty)
                {
                    return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, studioCheck);
                }

                if (this.movieRepo.Add(entity))
                {
                    if (this.movieRepo.SaveAll())
                    {
                        return this.Request.CreateResponse(HttpStatusCode.OK);
                    }
                }

                return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad formed JSON request");
            }
            catch (Exception ex)
            {
                return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("MovieKeep/Studios({id})")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                if (this.movieRepo.GetStudios().ToList().All(s => s.Id != id))
                {
                    return this.Request.CreateErrorResponse(
                        HttpStatusCode.NotFound,
                        "Studio with ID " + id + " not found.");
                }

                if (this.movieRepo.DeleteStudio(id) && this.movieRepo.SaveAll())
                {
                    return this.Request.CreateResponse(HttpStatusCode.NoContent);
                }

                return this.Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("MovieKeep/Studios({id})")]
        public HttpResponseMessage Patch(int id, [FromBody] StudiosModel model)
        {
            try
            {
                var entity = this.movieRepo.GetSingleStudio(id);
                if (entity == null)
                {
                    return this.Request.CreateResponse(HttpStatusCode.NotFound);
                }

                var parsedUserValue = this.modelFactory.ParsePatch(model, id);
                if (parsedUserValue == null)
                {
                    return this.Request.CreateResponse(HttpStatusCode.BadRequest);
                }

                var studioCheck = this.Validate.StudioNameCheck(entity.StudioName);

                if (studioCheck != string.Empty)
                {
                    return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, studioCheck);
                }

                if (this.movieRepo.SaveAll())
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
