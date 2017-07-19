using System;

using Movies.Frontend.Persistence;
using SQLite;
using System.IO;

namespace Movies.Frontend.iOS
{
    class SQLiteDb : ISQLiteDb
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "MyContacts.db3");

            return new SQLiteAsyncConnection(path);
        }
    }
}