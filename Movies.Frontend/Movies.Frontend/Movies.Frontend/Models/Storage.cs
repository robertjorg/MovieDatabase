using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Frontend.Models
{
    public class Storage
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Unique, MaxLength(255)]
        public string StorageName { get; set; }

        public string Url { get; set; }

        public DateTime DateModified { get; set; }

        public DateTime DateAdded { get; set; }
    }
}
