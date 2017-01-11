namespace Movie.Classes
{
    using System;

    public class MoviesOwned
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int MovieTitlesId { get; set; }

        public int StorageTypeId { get; set; }

        public int LoanId { get; set; }

        public DateTime EnterDt { get; set; }
    }
}
