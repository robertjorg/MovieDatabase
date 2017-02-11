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

    [Route("MovieKeep/Users")]
    public class UsersController : BaseApiController
    {
        public UsersController(IMovieRepository movieRepository) : base(movieRepository)
        {
        }

        public IEnumerable<UserModel> Get()
        {
            var results = this.movieRepo.GetAllUsers().ToList().Select(u => this.modelFactory.Create(u));

            return results;
        }

        [Route("MovieKeep/Users({id})")]
        public UserModel Get(int id)
        {
            return this.modelFactory.Create(this.movieRepo.GetSingleUser(id));
        }

        public HttpResponseMessage Post([FromBody]UserModel userModel)
        {
            try
            {
                var entity = this.modelFactory.Parse(userModel);

                if (entity == null)
                {
                    return this.Request.CreateErrorResponse(
                        HttpStatusCode.BadRequest,
                        "Bad formed JSON request.");
                }

                var userCheck = this.Validate.CheckUserName(entity.UserName);

                if (userCheck)
                {
                    if (this.movieRepo.Add(entity))
                    {
                        if (this.movieRepo.SaveAll())
                        {
                            return this.Request.CreateResponse(
                                HttpStatusCode.NoContent,
                                this.modelFactory.Create(entity));
                        }
                    }
                }

                return this.Request.CreateErrorResponse(HttpStatusCode.Conflict, "Username already exists");
            }
            catch (Exception e)
            {
                return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        [HttpDelete]
        [Route("MovieKeep/Users({id})")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                if (this.movieRepo.GetAllUsers().ToList().All(u => u.Id != id))
                {
                    return this.Request.CreateResponse(HttpStatusCode.NotFound);
                }

                if (this.movieRepo.DeleteUser(id) && this.movieRepo.SaveAll())
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
        [Route("MovieKeep/Users({id})")]
        public HttpResponseMessage Patch(int id, [FromBody] UserModel model)
        {
            try
            {
                var entity = this.movieRepo.GetSingleUser(id);
                if (entity == null)
                {
                    return this.Request.CreateResponse(HttpStatusCode.NotFound);
                }

                var parsedUserValue = this.modelFactory.ParsePatch(model);
                if (parsedUserValue == null)
                {
                    return this.Request.CreateResponse(HttpStatusCode.BadRequest);
                }

                if (!this.Validate.CheckUserNamePatch(model.UserName))
                {
                    return this.Request.CreateErrorResponse(
                        HttpStatusCode.BadRequest,
                        "A user with that username already exists");
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
