using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movie.API.Models
{
    using Movie.Classes;

    public class MoviesOwnedModel
    {
        public int Id { get; set; }

        public int MovieTitlesId { get; set; }

        public IEnumerable<MovieTitlesModel> MovieTitle { get; set; }
    }
}