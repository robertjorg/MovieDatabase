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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Types().Configure(c => c.Ignore("IsDirty"));
            modelBuilder.Entity<MoviesOwned>().HasMany(p => p.MovieTitles).WithMany().Map(
                m =>
                    {
                        m.MapLeftKey("MoviesOwnedId").MapRightKey("MovesOwnedId").ToTable("MoviesOwnedMovieTitles");
                    });
            base.OnModelCreating(modelBuilder);
        }
    }
}
