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

        //[Route("MovieKeep/StorageType")]
        //public HttpResponseMessage Post([FromBody] StorageTypeModel storageType)
        //{
            
        //}
    }
}
