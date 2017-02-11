using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movie.API.Models
{
    public class UserModel
    {
        public string Url { get; set; }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public DateTime OpenDt { get; set; }

        public DateTime LastLoginDt { get; set; }
    }
}