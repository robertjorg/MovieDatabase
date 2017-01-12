namespace Movie.Classes
{
    using System.Collections.Generic;

    public class Studios
    {
        public int Id { get; set; }

        public string StudioName { get; set; }

        public List<MovieTitles> Titles { get; set; }

        public int MovieTitlesId { get; set; }
    }
}
