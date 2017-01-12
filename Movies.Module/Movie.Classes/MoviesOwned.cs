namespace Movie.Classes
{
    using System;
    using System.Collections.Generic;

    public class MoviesOwned
    {
        public int Id { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }

        public List<MovieTitles> MovieTitles { get; set; }

        public int MovieTitlesId { get; set; }

        public StorageType StorageType { get; set; }

        public int StorageTypeId { get; set; }

        public List<Loan> Loan { get; set; }

        public int LoanId { get; set; }

        public DateTime EnterDt { get; set; }
    }
}
