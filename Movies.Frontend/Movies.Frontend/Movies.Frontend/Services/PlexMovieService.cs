using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Frontend.Services
{
    public class PlexMovieService : BaseMovieService
    {
        private string plexUrl;
        private HttpClient plexClient;
        private string login;
        private string password;

        public string Login
        {
            get
            {
                return login;
            }
            set
            {

            }
        }

        public PlexMovieService() : base()
        {
            this.plexUrl = "https://plex.tv/users/sign_in.json";
            this.plexClient = client;
        }

        public void SignIn()
        {

        }
    }
}
