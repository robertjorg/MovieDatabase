using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Frontend.Services
{
    public class BaseMovieService
    {
        public HttpClient client = new HttpClient();
    }
}
