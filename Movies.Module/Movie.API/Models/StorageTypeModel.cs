using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movie.API.Models
{
    public class StorageTypeModel
    {
        public int Id { get; set; }

        public string StorageName { get; set; }

        public string StorageUrl { get; set; }
    }
}