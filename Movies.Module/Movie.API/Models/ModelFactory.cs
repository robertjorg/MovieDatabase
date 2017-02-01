using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Movie.API.Models;
using Movie.Classes;

namespace Movie.API.Models
{
    using Movie.Classes;
    using System.Net.Http;
    using System.Web.Http.Routing;

    public class ModelFactory
    {
        //private UrlHelper urlHelper;

        //public ModelFactory(HttpRequestMessage request)
        //{
        //    this.urlHelper = new UrlHelper(request);
        //}

        public UserModel Create(User user)
        {
            return new UserModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                LastLoginDt = user.LastLoginDt,
                OpenDt = user.OpentDt
            };
        }
    }
}