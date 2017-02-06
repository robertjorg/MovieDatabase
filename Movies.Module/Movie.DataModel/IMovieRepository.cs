using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.DataModel
{
    using System.Linq;

    using Movie.Classes;

    public interface IMovieRepository
    {
        IQueryable<User> GetAllUsers();

        User GetSingleUser(int id);

        IQueryable<MoviesOwned> GetMoviesForUser(int userId);

        MovieTitles GetTitleForMovie(int movieTitlesId);

        IQueryable<MovieTitles> GetMovieTitles();
    }
}
