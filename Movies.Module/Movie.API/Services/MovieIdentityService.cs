using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movie.API.Services
{
    using System.Threading;

    public class MovieIdentityService : IMovieIdentityService
    {
        public string CurrentUser
        {
            get
            {
                return Thread.CurrentPrincipal.Identity;
            }
        }
    }
}