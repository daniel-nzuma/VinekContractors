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

       

        [Authorize(Roles = "admin")]
        public ActionResult AdminPanel()
        {

            return View();

        }

        [HttpPost]
        public JObject ValidateLogin(string email, string password)
        {
            JObject responseObject = new JObject();

            string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("verifylogin", sqlConnection);
            cmd.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = email;
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dataReader = cmd.ExecuteReader();

            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    var dbPaswordHash = dataReader[1].ToString();

                    if (Cryptography.verifyPassword(dbPaswordHash, password))
                    {
                        SignInUser(email, dataReader[0].ToString(), false);
                        responseObject.Add("success", true);
                        responseObject.Add("login_role", dataReader[0].ToString());

                    }
                    else
                    {
                        responseObject.Add("success", false);
                        responseObject.Add("message", "Password is invalid!");
                    }

                }
            }
            else 
            {
                responseObject.Add("success", false);
                responseObject.Add("message", "Email not found try again!");
            }

            dataReader.Close();
            sqlConnection.Close();

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