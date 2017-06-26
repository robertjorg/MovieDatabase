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
            return await this.connection.Table<MovieTitle>().ToListAsync();
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
