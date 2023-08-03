using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;

namespace Tracking
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user   
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider   
            // Configure the sign in cookie   

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Home/Login"),
                LogoutPath = new PathString("/Home/Index"),
                ExpireTimeSpan = TimeSpan.FromMinutes(15)
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            // Uncomment the following lines to enable logging in with third party login providers   
            //app.UseMicrosoftAccountAuthentication(   
            // clientId: "",   
            // clientSecret: "");   
            //app.UseTwitterAuthentication(   
            // consumerKey: "",   
            // consumerSecret: "");   
            //app.UseFacebookAuthentication(   
            // appId: "",   
            // appSecret: "");   
            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()   
            //{   
            // ClientId = "",   
            // ClientSecret = ""   
            //});   
        }
    }
}