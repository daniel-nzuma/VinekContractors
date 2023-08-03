using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using RestSharp;
using Newtonsoft.Json.Linq;

namespace Tracking.Controllers
{
    public class CarTrackingController : Controller
    {
        [Authorize]
        public ActionResult CarTrackingPanel()
        {
            return View();
        }

        [Authorize]
        public ActionResult GeoFence()
        {
            return View();
        }

        [Authorize]
        public ActionResult Commands()
        {
            return View();
        }

        [Authorize]
        public ActionResult Reports()
        {
            return View();
        }

        [HttpPost]
        public JsonObject FetchGPSPositions(int deviceID)
        {
            JsonArray jArray = new JsonArray();
            JsonObject positionsObject = new JsonObject();

            try
            {
                string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString;

                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("fetchPositionsByDeviceID",cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@deviceID", SqlDbType.Int).Value = deviceID;
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {

                                while (dr.Read())
                                {
                                    JsonObject jObject = new JsonObject();
                                    jObject.Add("id", dr["id"].ToString());
                                    jObject.Add("servertime", dr["servertime"].ToString());
                                    jObject.Add("devicetime", dr["devicetime"].ToString());
                                    jObject.Add("latitude", dr["latitude"].ToString());
                                    jObject.Add("longitude", dr["longitude"].ToString());
                                    jObject.Add("altitude", dr["altitude"].ToString());
                                    jObject.Add("speed", dr["speed"].ToString());
                                    jObject.Add("course", dr["course"].ToString());
                                    jObject.Add("accuracy", dr["accuracy"].ToString());

                                    jArray.Add(jObject);

                                }
                                positionsObject.Add("data", jArray);

                            }
                            else
                            {
                                JObject jObject = new JObject();
                                jArray.Add(jObject);

                                jObject.Add("success", false);
                                jObject.Add("message", "No match found.");
                            }
                        }
                    }

                }


            }
            catch (Exception ex)
            {
                JObject jObject = new JObject();
                jArray.Add(jObject);

                positionsObject.Add("success", false);
                positionsObject.Add("message", ex.Message.ToString());

            }

            return positionsObject;
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
                    
                        responseObject.Add("success", true);
                        responseObject.Add("login_role", dataReader[0].ToString());
                        responseObject.Add("success", false);
                        responseObject.Add("message", "Password is invalid!");
                    

                }
            }
            else
            {
                responseObject.Add("success", false);
                responseObject.Add("message", password);
            }

            dataReader.Close();
            sqlConnection.Close();

            return responseObject;

        }

    }
}