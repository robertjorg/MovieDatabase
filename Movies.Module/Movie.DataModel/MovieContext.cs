namespace Movie.DataModel
{
    using System.Data.Entity;

    using Movie.Classes;

    public class MovieContext : DbContext
    {
        public DbSet<MoviesOwned> MoviesOwned { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<StorageType> StorageType { get; set; }

        public DbSet<MovieTitles> MovieTitles { get; set; }

        public DbSet<Studios> Studios { get; set; }

        public DbSet<Loan> Loan { get; set; }
    }
}
