namespace Movie.Classes
{
    using System;

    using Microsoft.Build.Framework;

    using Movie.Classes.Interfaces;

    public class MovieTitles : IModificationHistory
    {
        public int Id { get; set; }

        [Required]
        public string MovieTitle { get; set; }

        public Studios Studios { get; set; }

        public int? StudiosId { get; set; }

        public string MovieDesc { get; set; }

        public DateTime? ReleaseDt { get; set; }

        public string ImdbUrl { get; set; }

        public DateTime? DateModified { get; set; }

        public DateTime? DateAdded { get; set; }

        public bool IsDirty { get; set; }
    }
}
