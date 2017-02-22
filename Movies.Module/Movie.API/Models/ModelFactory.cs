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
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                LastLoginDt = user.LastLoginDt,
                OpenDt = user.DateAdded
            };
        }

        public MoviesOwnedModel Create(MoviesOwned moviesOwned)
        {
            var storageType = this.movieRepository.GetSingleStorageType(moviesOwned.StorageTypeId);
            return new MoviesOwnedModel()
            {
                Id = moviesOwned.Id,
                MovieTitlesId = moviesOwned.MovieTitlesId,
                MovieTitle = moviesOwned.MovieTitles.MovieTitle,
                MovieDescription = moviesOwned.MovieTitles.MovieDesc,
                MovieStorageTypeName = storageType.StorageName
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

        public StorageTypeModel Create(StorageType storageType)
        {
            return new StorageTypeModel()
            {
                Id = storageType.Id,
                StorageName = storageType.StorageName,
                StorageUrl = storageType.Url
            };
        }

        public StudiosModel Create(Studios studios)
        {
            return new StudiosModel() { Id = studios.Id, StudioName = studios.StudioName };
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

        public MovieTitles ParsePatch(MovieTitlesModel model, int id)
        {
            try
            {
                MovieTitles title = this.movieRepository.GetTitleForMovie(id);

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

        public MoviesOwned ParsePatch(MoviesOwnedModel model, int userId, int id)
        {
            try
            {
                MoviesOwned owned = this.movieRepository.GetSingleMovieOwned(userId, id);

                if (model.MovieStorageTypeName != null)
                {
                    var checkStorage = this.movieRepository.GetStorageType()
                        .FirstOrDefault(s => s.StorageName == model.MovieStorageTypeName);
                    if (checkStorage != null)
                    {
                        owned.StorageTypeId = checkStorage.Id;
                    }
                }

                owned.DateModified = DateTime.Now;

                return owned;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public User Parse(UserModel model)
        {
            try
            {
                var user = new User();

                if (model.LastName != null && model.FirstName != null && model.UserName != null)
                {
                    user.LastName = model.LastName;
                    user.FirstName = model.FirstName;
                    user.UserName = model.UserName;
                    user.LastLoginDt = DateTime.Now;
                    user.DateAdded = DateTime.Now;
                    user.DateModified = DateTime.Now;

                    return user;
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

        public User ParsePatch(UserModel model, int id)
        {
            try
            {
                User user = this.movieRepository.GetSingleUser(id);

                if (model.LastName != null)
                {
                    user.LastName = model.LastName;
                }

                if (model.FirstName != null)
                {
                    user.FirstName = model.FirstName;
                }

                if (model.UserName != null)
                {
                    user.UserName = model.UserName;
                }

                user.DateModified = DateTime.Now;
                
                return user;
            }
            catch
            {
                return null;
            }
        }

        public StorageType ParsePatch(StorageTypeModel model, int id)
        {
            try
            {
                StorageType storageType = this.movieRepository.GetSingleStorageType(id);

                if (model.StorageName != null)
                {
                    storageType.StorageName = model.StorageName;
                }

                if (model.StorageUrl != null)
                {
                    storageType.Url = model.StorageUrl;
                }

                storageType.DateModified = DateTime.Now;

                return storageType;
            }
            catch
            {
                return null;
            }
        }

        public MoviesOwned Parse(MoviesOwnedModel model, int userId)
        {
            try
            {
                var moviesOwned = new MoviesOwned { UserId = userId };

                if (model.MovieTitle == null)
                {
                    return null;
                }

                var movieTitleId =
                    this.movieRepository.GetMovieTitles().FirstOrDefault(t => t.MovieTitle == model.MovieTitle);
                if (movieTitleId != null)
                {
                    moviesOwned.MovieTitlesId = movieTitleId.Id;
                }

                var storageType = this.movieRepository.GetStorageType().FirstOrDefault(s => s.StorageName == model.MovieStorageTypeName);

                if (storageType != null)
                {
                    moviesOwned.StorageTypeId = storageType.Id;
                }

                moviesOwned.MovieTitles = this.movieRepository.GetMovieTitles().FirstOrDefault(t => t.MovieTitle == model.MovieTitle);
                moviesOwned.DateAdded = DateTime.Now;
                moviesOwned.DateModified = DateTime.Now;

                return moviesOwned;
            }
            catch
            {
                return null;
            }
        }

        public StorageType Parse(StorageTypeModel model)
        {
            try
            {
                var storageType = new StorageType();

                if (model.StorageName == null || model.StorageUrl == null)
                {
                    return null;
                }

                storageType.StorageName = model.StorageName;
                storageType.Url = model.StorageUrl;
                storageType.DateAdded = DateTime.Now;
                storageType.DateModified = DateTime.Now;

                return storageType;
            }
            catch
            {
                return null;
            }
        }

        public Studios Parse(StudiosModel model)
        {
            try
            {
                var studio = new Studios();

                if (model.StudioName == null)
                {
                    return null;
                }

                studio.StudioName = model.StudioName;
                studio.DateAdded = DateTime.Now;
                studio.DateModified = DateTime.Now;

                return studio;
            }
            catch
            {
                return null;
            }
        }

        public Studios ParsePatch(StudiosModel model, int id)
        {
            try
            {
                Studios studio = this.movieRepository.GetSingleStudio(id);

                if (model.StudioName != null)
                {
                    studio.StudioName = model.StudioName;
                }
                
                studio.DateModified = DateTime.Now;

                return studio;
            }
            catch
            {
                return null;
            }
        }
    }
}