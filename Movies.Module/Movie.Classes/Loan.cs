namespace Movie.Classes
{
    using System;
    using System.Collections.Generic;

    public class Loan
    {
        public int Id { get; set; }

        public DateTime LoanDt { get; set; }

        public DateTime ReturnDt { get; set; }

        public string LoanToName { get; set; }

        public List<MoviesOwned> MoviesLoaned { get; set; }
    }
}
