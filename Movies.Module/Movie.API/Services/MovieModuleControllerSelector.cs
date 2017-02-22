//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace Movie.API.Services
//{
//    using System.Net.Http;
//    using System.Web.Http;
//    using System.Web.Http.Controllers;
//    using System.Web.Http.Dispatcher;
//    public class MovieModuleControllerSelector : DefaultHttpControllerSelector
//    {
//        private HttpConfiguration config;

//        public MovieModuleControllerSelector(HttpConfiguration config)
//            : base(config)
//        {
//            this.config = config;
//        }

//        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
//        {
//            var controllers = this.GetControllerMapping(); // will want different named controllers for different versions

//            var routeData = request.GetRouteData();

//            var controllerName = (string)routeData.Values["controller"];

//            HttpControllerDescriptor descriptor;

//            if (controllers.TryGetValue(controllerName, out descriptor))
//            {
//                var version = "2";

//                var newName = string.Concat(controllerName, "V", version);

//                HttpControllerDescriptor versionedDescriptor;

//                if (controllers.TryGetValue(newName, out versionedDescriptor))
//                {
//                    return versionedDescriptor;
//                }

//                return descriptor;
//            }

//            return null;
//        }
//    }
//}