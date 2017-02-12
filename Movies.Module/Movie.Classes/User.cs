namespace Movie.Classes
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Build.Framework;

    using Movie.Classes.Interfaces;

    public class User : IModificationHistory
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string UserName { get; set; }

        public DateTime LastLoginDt { get; set; }

        public List<MoviesOwned> MoviesOwned { get; set; }

        public DateTime? DateModified { get; set; }

        public DateTime? DateAdded { get; set; }

        public bool IsDirty { get; set; }
    }
}
