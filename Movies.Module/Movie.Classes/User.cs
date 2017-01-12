namespace Movie.Classes
{
    using System;
    using System.Collections.Generic;

    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime OpentDt { get; set; }

        public DateTime LastLoginDt { get; set; }

        public List<MoviesOwned> MoviesOwned { get; set; }
    }
}
