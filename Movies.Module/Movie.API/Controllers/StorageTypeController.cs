using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Movie.API.Controllers
{
    using Microsoft.Ajax.Utilities;

    using Models;
    using Movie.API.Services;
    using Movie.DataModel;

    [Route("MovieKeep/StorageType")]
    public class StorageTypeController : BaseApiController
    {
        private IMovieIdentityService identityService;

        public StorageTypeController(IMovieRepository movieRepository, IMovieIdentityService identityService) : base(movieRepository)
        {
            this.identityService = identityService;
        }

        public IEnumerable<StorageTypeModel> Get()
        {
            var results = this.movieRepo.GetStorageType().ToList().Select(m => this.modelFactory.Create(m));

            return results;
        }

        [Route("MovieKeep/StorageType({id})")]
        public HttpResponseMessage Get(int id)
        {
            var results = this.movieRepo.GetSingleStorageType(id);

            if (results == null)
            {
                return this.Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return this.Request.CreateResponse(
                HttpStatusCode.OK,
                this.modelFactory.Create(this.movieRepo.GetSingleStorageType(id)));
        }

        [HttpPost]
        [Route("MovieKeep/StorageType")]
        public HttpResponseMessage Post([FromBody] StorageTypeModel storageType)
        {
            try
            {
                var entity = this.modelFactory.Parse(storageType);

                if (entity == null)
                {
                    return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad formed JSON request.");
                }

                var storageTypeCheck = this.Validate.StorageTypeCheck(entity.StorageName, entity.Url);

                if (storageTypeCheck != string.Empty)
                {
                    return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, storageTypeCheck);
                }

                if (this.movieRepo.Add(entity))
                {
                    if (this.movieRepo.SaveAll())
                    {
                        return this.Request.CreateResponse(HttpStatusCode.Created, this.modelFactory.Create(entity));
                    }
                }

                return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad formed JSON request.");
            }
            catch (Exception e)
            {
                return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        [HttpDelete]
        [Route("MovieKeep/StorageType({id})")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                if (this.movieRepo.GetStorageType().ToList().All(mi => mi.Id != id))
                {
                    return this.Request.CreateResponse(HttpStatusCode.NotFound);
                }

                if (this.movieRepo.DeleteStorageType(id) && this.movieRepo.SaveAll())
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
        [Route("MovieKeep/StorageType({id})")]
        public HttpResponseMessage Patch(int id, [FromBody] StorageTypeModel model)
        {
            try
            {
                var entity = this.movieRepo.GetSingleStorageType(id);
                if (entity == null)
                {
                    return this.Request.CreateResponse(HttpStatusCode.NotFound);
                }

                var parsedStorage = this.modelFactory.ParsePatch(model);
                if (parsedStorage == null)
                {
                    return this.Request.CreateResponse(HttpStatusCode.BadRequest);
                }

                var storageCheck = this.Validate.StorageTypeCheck(parsedStorage.StorageName, parsedStorage.Url);

                if (storageCheck == null)
                {
                    return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, storageCheck);
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
