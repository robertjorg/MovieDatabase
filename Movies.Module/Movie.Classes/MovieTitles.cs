namespace Movie.Classes
{
    using System;

    public class MovieTitles
    {
        public int Id { get; set; }

        public string MovieTitle { get; set; }

        public int StudioId { get; set; }

        public string MovieDesc { get; set; }

        public DateTime ReleaseDt { get; set; }

        public string ImdbUrl { get; set; }
    }
}
