using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Tracking.Helpers;
using System.Net;
using System.Net.Mail;

namespace Tracking.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult PrivacyPolicy()
        {
            return View();
        }


        [HttpPost]
        public JObject SendEmail(string fromAddress, string subject,string message)
        {
            JObject responseObject = new JObject();

            MailAddress to = new MailAddress("nzuma.daniel@eclectics.io");
            MailAddress from = new MailAddress(fromAddress);

            MailMessage email = new MailMessage(from, to);
            email.Subject = subject;
            email.Body = message + $"from {from}";

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential("nzuma.daniel@eclectics.io", "Fdunlastborn9");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;
            

            try
            {
                /* Send method called below is what will send off our email 
                 * unless an exception is thrown.
                 */
                smtp.Send(email);
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return responseObject;

        }


        [HttpPost]
        [Authorize(Roles = "admin")]
        public JObject CreateUser(string email, string password)
        {
            JObject responseObject = new JObject();

            responseObject.Add("success", true);
            responseObject.Add("email", email);
            responseObject.Add("password", password);


            return responseObject;

        }

        private void SignInUser(string email, string role, bool isPersistent)
        {
            var claims = new List<Claim>();
            try
            {
                /* if(role != "user")
                 {
                     claims.Add(new Claim(ClaimTypes.Role, role));
                 }*/
                claims.Add(new Claim(ClaimTypes.Role, role));
                claims.Add(new Claim(ClaimTypes.Email, email));
                var claimIdenties = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                var ctx = Request.GetOwinContext();
                var authenticationManager = ctx.Authentication;
                authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, claimIdenties);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            try
            {
                var ctx = Request.GetOwinContext();
                var authenticationManager = ctx.Authentication;
                authenticationManager.SignOut();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return this.RedirectToAction("Login", "Home");
        }
    }
}