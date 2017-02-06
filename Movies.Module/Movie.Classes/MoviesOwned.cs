namespace Movie.Classes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    using Movie.Classes.Interfaces;

    [Table("MoviesOwned")]
    public class MoviesOwned : IModificationHistory
    {
        public int Id { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }

        public MovieTitles MovieTitles { get; set; }

        public int MovieTitlesId { get; set; }

        public StorageType StorageType { get; set; }

        public int? StorageTypeId { get; set; }

        public List<Loan> Loan { get; set; }

        public DateTime DateModified { get; set; }

        public DateTime DateAdded { get; set; }

        public bool IsDirty { get; set; }
    }
}
