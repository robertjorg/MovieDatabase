using Movies.Frontend.Models;
using Movies.Frontend.Persistence;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Frontend.ViewModels
{
    public class BaseMovieTitleStore
    {
        private SQLiteAsyncConnection connection;

        public BaseMovieTitleStore(ISQLiteDb db)
        {
            this.connection = db.GetConnection();
            this.connection.CreateTableAsync<MovieTitle>();
            this.connection.CreateTableAsync<Storage>();
        }

        public async Task<IEnumerable<MovieTitle>> GetMovieTitlesAsync()
        {
            var returnable = await this.connection.Table<MovieTitle>().ToListAsync();
            return returnable.OrderBy(mt => mt.Title).ThenBy(s => s.StorageType);
        }

        public async Task<IEnumerable<MovieTitle>> GetSearchMoviesAsync(string searchString)
        {
            var returnableMovie = await this.connection.Table<MovieTitle>().Where(n => n.Title.StartsWith(searchString)).ToListAsync();
            var returnableStorage = await this.connection.Table<MovieTitle>().Where(s => s.StorageType.StartsWith(searchString)).ToListAsync();

            List<MovieTitle> returnable = new List<MovieTitle>();

            foreach (MovieTitle m in returnableMovie)
            {
                returnable.Add(m);
            }

            foreach(MovieTitle s in returnableStorage)
            {
                returnable.Add(s);
            }

            return returnable.OrderBy(mt => mt.Title).ThenBy(s => s.StorageType);
        }

        public async Task<MovieTitle> GetMovie(int id)
        {
            return await this.connection.FindAsync<MovieTitle>(id);
        }

        public async Task AddMovieTitle(MovieTitle movieTitle)
        {
            await this.connection.InsertAsync(movieTitle);
        }

        public async Task UpdateMovieTitle(MovieTitle movieTitle)
        {
            await this.connection.UpdateAsync(movieTitle);
        }

        public async Task DeleteMovieTitle(MovieTitle movieTitle)
        {
            await this.connection.DeleteAsync(movieTitle);
        }
    }
}
