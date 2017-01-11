namespace Movie.Classes
{
    using System;

    public class Loan
    {
        public int Id { get; set; }

        public DateTime LoanDt { get; set; }

        public DateTime ReturnDt { get; set; }

        public string LoanToName { get; set; }
    }
}
