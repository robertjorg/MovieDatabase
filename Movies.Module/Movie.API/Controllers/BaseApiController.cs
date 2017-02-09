using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Movie.API.Controllers
{
    using Movie.API.Models;
    using Movie.API.Validations;
    using Movie.DataModel;

    public abstract class BaseApiController : ApiController
    {
        private IMovieRepository movieRepository;

        private ModelFactory modelFact;

        private Validations validations;

        public BaseApiController(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        protected ModelFactory modelFactory
        {
            get
            {
                if (this.modelFact == null)
                {
                    this.modelFact = new ModelFactory(this.Request, this.movieRepository);
                }
                return this.modelFact;
            }
        }

        protected IMovieRepository movieRepo
        {
            get
            {
                return this.movieRepository;
            }
        }

        protected Validations Validate
        {
            get
            {
                if (this.validations == null)
                {
                    this.validations = new Validations(this.movieRepository);
                }
                return this.validations;
            }
        }
    }
}
