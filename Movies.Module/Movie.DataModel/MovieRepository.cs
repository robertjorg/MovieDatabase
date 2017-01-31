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
        private MovieContext db;

        public MovieRepository(MovieContext db)
        {
            this.db = db;
        }

        public IQueryable<User> GetAllUsers()
        {
            return this.db.User;
        }

        public User GetSingleUser(int id)
        {
            return this.db.User.FirstOrDefault(u => u.Id == id);
        }
    }
}
