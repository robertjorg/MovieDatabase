using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Frontend.Models
{
    public class MovieTitle
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(255)]
        public string Title { get; set; }

        public string MovieDesc { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public string ImdbUrl { get; set; }

        public DateTime DateModified { get; set; }

        public DateTime DateAdded { get; set; }
    }
}
