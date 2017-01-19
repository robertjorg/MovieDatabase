namespace Movie.Classes
{
    using System;
    using System.Runtime.CompilerServices;

    using Microsoft.Build.Framework;

    using Movie.Classes.Interfaces;
    public class StorageType : IModificationHistory
    {
        public int Id { get; set; }

        [Required]
        public string StorageName { get; set; }

        public string Url { get; set; }

        // will this be the place to save login info if I can get APis?
        public DateTime DateModified { get; set; }

        public DateTime DateAdded { get; set; }

        public bool IsDirty { get; set; }
    }
}
