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
    public class BaseStorageStore
    {
        private SQLiteAsyncConnection connection;

        public BaseStorageStore(ISQLiteDb db)
        {
            this.connection = db.GetConnection();
            this.connection.CreateTableAsync<Storage>();
        }

        public async Task<IEnumerable<Storage>> GetStorageAsync()
        {
            return await this.connection.Table<Storage>().ToListAsync();
        }

        public async Task<Storage> GetStorage(int id)
        {
            return await this.connection.FindAsync<Storage>(id);
        }

        public async Task AddStorageType(Storage storage)
        {
            await this.connection.InsertAsync(storage);
        }

        public async Task UpdateStorageType(Storage storage)
        {
            await this.connection.UpdateAsync(storage);
        }

        public async Task DeleteStorage(Storage storage)
        {
            await this.connection.DeleteAsync(storage);
        }
    }
}
