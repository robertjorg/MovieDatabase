namespace Movies.Frontend.Persistence
{
    using SQLite;

    public interface ISQLiteDb
    {
        SQLiteAsyncConnection GetConnection();
    }
}
