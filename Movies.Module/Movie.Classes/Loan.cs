namespace Movie.Classes
{
    using System;
    using System.Collections.Generic;

    using Movie.Classes.Interfaces;

    public class Loan : IModificationHistory
    {
        public int Id { get; set; }

        public DateTime LoanDt { get; set; }

        public DateTime? ReturnDt { get; set; }

        public string LoanToFirstName { get; set; }

        public string LoanToLastName { get; set; }

        public List<MoviesOwned> MoviesLoaned { get; set; }

        public int MoviesOwnedId { get; set; }

        public DateTime DateModified { get; set; }

        public DateTime DateAdded { get; set; }

        public bool IsDirty { get; set; }
    }
}
