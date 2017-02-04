using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Movie.API.Models;
using Movie.Classes;

namespace Movie.API.Models
{
    using Movie.Classes;
    using System.Net.Http;
    using System.Web.Http.Routing;

    public class ModelFactory
    {
        //private UrlHelper urlHelper;

        //public ModelFactory(HttpRequestMessage request)
        //{
        //    this.urlHelper = new UrlHelper(request);
        //}

        public UserModel Create(User user)
        {
            return new UserModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                LastLoginDt = user.LastLoginDt,
                OpenDt = user.OpentDt
            };
        }

        public MoviesOwnedModel Create(MoviesOwned moviesOwned)
        {
            return new MoviesOwnedModel()
            {
                Id = moviesOwned.Id,
                MovieTitlesId = moviesOwned.MovieTitlesId,
                //MovieTitles = moviesOwned.MovieTitle.MovieTitle.Select(t => Create(t));
            };
        }

        public MovieTitlesModel Create(MovieTitles movieTitles)
        {
            return new MovieTitlesModel()
            {
                Id = movieTitles.Id,
                MovieTitle = movieTitles.MovieTitle,
                MovieDesc = movieTitles.MovieDesc,
                ReleaseDate = movieTitles.ReleaseDt,
                ImdbUrl = movieTitles.ImdbUrl
            };
        }
    }
}