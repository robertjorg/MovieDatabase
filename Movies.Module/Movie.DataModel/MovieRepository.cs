﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.DataModel
{
    using System.Data.Entity;
    using System.Data.Entity.Migrations;

    using Movie.Classes;
    

    public class MovieRepository : IMovieRepository
    {
        private MovieContext movieContext;

        public MovieRepository(MovieContext movieContext)
        {
            this.movieContext = movieContext;
        }

        public IQueryable<User> GetAllUsers()
        {
            return this.movieContext.User;
        }

        public User GetSingleUser(int id)
        {
            return this.movieContext.User.FirstOrDefault(u => u.Id == id);
        }

        public IQueryable<MoviesOwned> GetMoviesForUser(int userId)
        {
             return this.movieContext.MoviesOwned.Include("MovieTitles").Where(u => u.UserId == userId);
            //return this.movieContext.MoviesOwned.Where(u => u.UserId == userId);
        }

        public MoviesOwned GetSingleMovieOwned(int userId, int id)
        {
            // return this.movieContext.MoviesOwned.Where(u => u.UserId == userId && u.Id == id).FirstOrDefault();
            return this.movieContext.MoviesOwned.Include("MovieTitles").Where(u => u.UserId == userId && u.Id == id).FirstOrDefault();
        }

        public MovieTitles GetTitleForMovie(int movieTitlesId)
        {
            return this.movieContext.MovieTitles.FirstOrDefault(m => m.Id == movieTitlesId);
        }

        public IQueryable<MovieTitles> GetMovieTitles()
        {
            return this.movieContext.MovieTitles;
        }

        public MovieTitles GetTitleForOwnedMovie(int userId, int moviesOwnedId, int id)
        {
            return this.movieContext.MovieTitles.FirstOrDefault(m => m.Id == id);
        }

        public IQueryable<StorageType> GetStorageType()
        {
            return this.movieContext.StorageType;
        }

        public StorageType GetSingleStorageType(int id)
        {
            return this.movieContext.StorageType.FirstOrDefault(s => s.Id == id);
        }

        public IEnumerable<Studios> GetStudios()
        {
            return this.movieContext.Studios;
        }

        public Studios GetSingleStudio(int id)
        {
            return this.movieContext.Studios.FirstOrDefault(s => s.Id == id);
        }

        public bool Add(MovieTitles title)
        {
            try
            {
                this.movieContext.MovieTitles.Add(title);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool Add(User user)
        {
            try
            {
                this.movieContext.User.Add(user);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool Add(StorageType storageType)
        {
            try
            {
                this.movieContext.StorageType.Add(storageType);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool Add(Studios studios)
        {
            try
            {
                this.movieContext.Studios.Add(studios);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool Add(MoviesOwned moviesOwned)
        {
            try
            {
                this.movieContext.MoviesOwned.Add(moviesOwned);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool DeleteMovieTitle(int id)
        {
            var titleToDelete = this.movieContext.MovieTitles.FirstOrDefault(t => t.Id == id);
            if (titleToDelete != null)
            {
                this.movieContext.MovieTitles.Remove(titleToDelete);
                return true;
            }

            return false;
        }

        public bool DeleteUser(int id)
        {
            var userToDelete = this.movieContext.User.FirstOrDefault(t => t.Id == id);
            if (userToDelete != null)
            {
                this.movieContext.User.Remove(userToDelete);
                return true;
            }

            return false;
        }

        public bool DeleteStorageType(int id)
        {
            var storageToDelete = this.movieContext.StorageType.FirstOrDefault(s => s.Id == id);
            if (storageToDelete != null)
            {
                this.movieContext.StorageType.Remove(storageToDelete);
                return true;
            }

            return false;
        }

        public bool DeleteStudio(int id)
        {
            var studioToDelete = this.movieContext.Studios.FirstOrDefault(s => s.Id == id);
            if (studioToDelete != null)
            {
                this.movieContext.Studios.Remove(studioToDelete);
                return true;
            }

            return false;
        }

        public bool DeleteMovieOwned(int userId, int id)
        {
            var userMovies = this.movieContext.User.FirstOrDefault(u => u.Id == userId);
            if (userMovies != null)
            {
                var movieOwnedToDelete =
                    userMovies.MoviesOwned.FirstOrDefault(m => m.Id == id);
                this.movieContext.MoviesOwned.Remove(movieOwnedToDelete);
            }

            return false;
        }

        public bool SaveAll()
        {
            return this.movieContext.SaveChanges() > 0;
        }

        public bool UpdateMovieTitle(MovieTitles movieTitles)
        {
            var dbset = this.movieContext.MovieTitles;
            var context = this.movieContext.Entry(movieTitles);
            if (context.State == EntityState.Detached)
            {
                dbset.Attach(movieTitles);
            }

            context.State = EntityState.Modified;

            return true;
        }
    }
}
