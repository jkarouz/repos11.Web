using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using repos11.Web.Utilities.Security;
using System;

namespace repos11.Web
{
    public partial class Startup
    {
        public void ConfigureAuthDevelopment(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = ConstansAuthentication.DeafultCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider(),
                CookieName = ConstansAuthentication.DeafultCookieName,
                CookieHttpOnly = true,
                ExpireTimeSpan = TimeSpan.FromHours(12),
            });
        }
    }
}