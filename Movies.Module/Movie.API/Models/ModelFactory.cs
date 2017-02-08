using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Movie.API.Models;

namespace Movie.API.Models
{
    using System.Net.Http;
    using System.Web.Http.Routing;
    using Movie.Classes;
    using Movie.DataModel;

    public class ModelFactory
    {
        private UrlHelper urlHelper;

        private IMovieRepository movieRepository;

        public ModelFactory(HttpRequestMessage request, IMovieRepository movieRepository)
        {
            this.urlHelper = new UrlHelper(request);
            this.movieRepository = movieRepository;
        }

        public UserModel Create(User user)
        {
            return new UserModel()
            {
                Url = this.urlHelper.Link("Users", new { id = user.Id }), // do I want to have the URL for all of the endpoints?
                Id = user.Id,
                FullName = user.FirstName + " " + user.LastName,
                UserName = user.UserName,
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
                MovieTitle = moviesOwned.MovieTitles.MovieTitle,
                MovieDescription = moviesOwned.MovieTitles.MovieDesc
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

        public MovieTitles Parse(MovieTitlesModel model)
        {
            try
            {
                var title = new MovieTitles();

                if (model.MovieTitle != null)
                {
                    title.MovieTitle = model.MovieTitle;
                    if (model.MovieDesc != null)
                    {
                        title.MovieDesc = model.MovieDesc;
                    }

                    if (model.ImdbUrl != null)
                    {
                        title.ImdbUrl = model.ImdbUrl;
                    }

                    if (model.ReleaseDate != null)
                    {
                        title.ReleaseDt = model.ReleaseDate;
                    }

                    title.DateAdded = DateTime.Now;
                    title.DateModified = DateTime.Now;

                    return title;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}