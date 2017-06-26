using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Frontend.Models
{
    public class MovieTitle
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Unique]
        public string TitleReleaseDate { get; set; }

        [MaxLength(255)]
        public string Title { get; set; }

        public string MovieDesc { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public string ImdbUrl { get; set; }

        public string StorageType { get; set; }

        public DateTime DateModified { get; set; }

        public DateTime DateAdded { get; set; }
    }
}
