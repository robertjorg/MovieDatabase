namespace Movie.Classes
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Build.Framework;

    using Movie.Classes.Interfaces;

    public class Studios : IModificationHistory
    {
        public int Id { get; set; }

        [Required]
        public string StudioName { get; set; }

        public List<MovieTitles> Titles { get; set; }

        public DateTime? DateModified { get; set; }

        public DateTime? DateAdded { get; set; }

        public bool IsDirty { get; set; }
    }
}
