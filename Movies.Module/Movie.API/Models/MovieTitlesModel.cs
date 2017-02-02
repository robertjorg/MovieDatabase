using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movie.API.Models
{
    public class MovieTitlesModel
    {
        public int Id { get; set; }

        public string MovieTitle { get; set; }

        public string MovieDesc { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public string ImdbUrl { get; set; }
    }
}