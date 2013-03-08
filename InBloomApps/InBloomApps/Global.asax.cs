/*
 * Copyright 2012-2013 inBloom, Inc. and its affiliates.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using InBloomApps.Global;

namespace InBloomApps
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //unregister Web Forms view engine so that the view locator doesn't search for view locations for Web Forms
            var viewEngines = System.Web.Mvc.ViewEngines.Engines;
            var webformsEngine = viewEngines.OfType<WebFormViewEngine>().FirstOrDefault();
            if (webformsEngine != null)
                viewEngines.Remove(webformsEngine);

            ValueProviderFactories.Factories.Remove(ValueProviderFactories.Factories.OfType<JsonValueProviderFactory>().FirstOrDefault());
            ValueProviderFactories.Factories.Add(new JsonLargeResultValueProviderFactory());
            //JsonValueProviderFactory
        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

            //Ensure SessionID in order to prevent the folloing exception
            //when the Application Pool Recycles
            //[HttpException]: Session state has created a session id, but cannot
            //    save it because the response was already flushed by 
            string id = Session.SessionID;
        }

        protected void Application_OnPostAuthenticateRequest(Object sender, EventArgs e)
        {
            IPrincipal contextUser = Context.User;

            if (contextUser.Identity.AuthenticationType == "Forms")
            {
                // determine role name
                string[] roles = new string[1];
                roles[0] = "user";

                // attach to context
                HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(User.Identity, roles);
                Thread.CurrentPrincipal = HttpContext.Current.User;
            }
        }
    }
}