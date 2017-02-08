using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.DataModel
{
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
            this.movieContext.SaveChanges();
            return true;
        }
    }
}
