using System;
using System.IO;
using SQLite;
using Xamarin.Forms;
using Movies.Frontend.Droid;
using Movies.Frontend.Persistence;


[assembly: Dependency(typeof(SQLiteDb))]

namespace Movies.Frontend.Droid
{
    public class SQLiteDb : ISQLiteDb
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "MovieDataBase.db3");

            return new SQLiteAsyncConnection(path);
        }
    }
}