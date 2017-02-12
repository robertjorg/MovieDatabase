using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movie.API.Validations
{
    using Movie.API.Models;
    using Movie.DataModel;

    public class Validations
    {
        private IMovieRepository movieRepository;

        public Validations(IMovieRepository movieRepo)
        {
            this.movieRepository = movieRepo;
        }

        public bool CheckMovieTitles(string title)
        {
            var checkTitle =
                this.movieRepository.GetMovieTitles().ToList().Any(t => t.MovieTitle == title);
            if (!checkTitle)
            {
                return true;
            }
            return false;
        }

        public bool CheckMovieTitlesPatch(string movieTitle, string sentType = "")
        {
            int titleCount;

            if (sentType == "patch")
            {
                titleCount = this.movieRepository.GetMovieTitles().Count(mt => mt.MovieTitle == movieTitle);
            }
            else
            {
                titleCount = this.movieRepository.GetMovieTitles().Count(mt => mt.MovieTitle == movieTitle) + 1;
            }
            
            return titleCount <= 1;
        }

        public bool CheckUserName(string userName)
        {
            var checkUserName = this.movieRepository.GetAllUsers().ToList().Any(n => n.UserName == userName);
            if (!checkUserName)
            {
                return true;
            }
            return false;
        }

        public bool CheckUserNamePatch(string userName)
        {
            var userNameCount = this.movieRepository.GetAllUsers().Count(u => u.UserName == userName) + 1;
            return userNameCount <= 1;
        }

        public string StorageTypeCheck(string storageName, string url)
        {
            string returnValue = string.Empty;
            var storageNameCount = this.movieRepository.GetStorageType().Count(sn => sn.StorageName == storageName) + 1;
            var storageUrlCount = this.movieRepository.GetStorageType().Count(su => su.Url == url) + 1;

            if (storageNameCount > 1)
            {
                returnValue = "Storage name already exists.";
            }

            if (storageUrlCount > 1)
            {
                if (returnValue != string.Empty)
                {
                    returnValue = returnValue + " Storage URL already exists.";
                }
                else
                {
                    returnValue = "Storage URL already exists.";
                }
                
            }

            return returnValue;
        }
    }
}