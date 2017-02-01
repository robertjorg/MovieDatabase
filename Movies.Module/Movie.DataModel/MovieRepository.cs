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
    }
}
