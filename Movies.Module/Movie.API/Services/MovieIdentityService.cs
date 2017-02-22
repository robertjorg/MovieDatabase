using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movie.API.Services
{
    using System.Security.Principal;
    using System.Threading;

    public class MovieIdentityService : IMovieIdentityService
    {
        public IIdentity CurrentUser
        {
            get
            {
                return Thread.CurrentPrincipal.Identity;
            }
        }
    }
}