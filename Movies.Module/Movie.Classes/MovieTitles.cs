namespace Movie.Classes
{
    using System;

    public class MovieTitles
    {
        public int Id { get; set; }

        public string MovieTitle { get; set; }

        public Studios Studio { get; set; }

        public int StudiosId { get; set; }

        public string MovieDesc { get; set; }

        public DateTime ReleaseDt { get; set; }

        public string ImdbUrl { get; set; }

        public MoviesOwned Movie { get; set; }

        public int MoviesOwnedId { get; set; }
    }
}
