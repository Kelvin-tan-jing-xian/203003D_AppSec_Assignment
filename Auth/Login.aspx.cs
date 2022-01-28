using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;

namespace _203003D_AppSec_Assignment
{
    public partial class Login : System.Web.UI.Page
    {
        string MYDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MYDBConnection"].ConnectionString;
        static string finalHash;
        static string salt;
        byte[] Key;
        byte[] IV;
        static String otp;

        public class MyObject
        {
            public string success { get; set; }
            public List<string> ErrorMessage { get; set; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public bool ValidateCaptcha()
        {
            bool result = true;

            //When user submits the recaptcha form, the user gets a response POST parameter. 
            //captchaResponse consist of the user click pattern. Behaviour analytics! AI :) 
            string captchaResponse = Request.Form["g-recaptcha-response"];

            //To send a GET request to Google along with the response and Secret key.
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create
           (" https://www.google.com/recaptcha/api/siteverify?secret=6LeaijwdAAAAANjP45faDlNJOS6F1KP9N2d0fgA7 &response=" + captchaResponse);


            try
            {

                //Codes to receive the Response in JSON format from Google Server
                using (WebResponse wResponse = req.GetResponse())
                {
                    using (StreamReader readStream = new StreamReader(wResponse.GetResponseStream()))
                    {
                        //The response in JSON format
                        string jsonResponse = readStream.ReadToEnd();


                        JavaScriptSerializer js = new JavaScriptSerializer();

                        //Create jsonObject to handle the response e.g success or Error
                        //Deserialize Json
                        MyObject jsonObject = js.Deserialize<MyObject>(jsonResponse);

                        //Convert the string "False" to bool false or "True" to bool true
                        result = Convert.ToBoolean(jsonObject.success);//

                    }
                }

                return result;
            }
            catch (WebException ex)
            {
                throw ex;
            }
        }

        protected void LoginMe(object sender, EventArgs e)
        {
            string pwd = tb_pwd.Text.ToString().Trim();
            string userid = tb_email.Text.ToString().Trim();
            SHA512Managed hashing = new SHA512Managed();
            string dbHash = getDBHash(userid);
            string dbSalt = getDBSalt(userid);
            if (ValidateCaptcha())
            {
                try
                {
                    if (dbSalt != null && dbSalt.Length > 0 && dbHash != null && dbHash.Length > 0)
                    {
                        string pwdWithSalt = pwd + dbSalt;
                        byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));
                        string userHash = Convert.ToBase64String(hashWithSalt);


                        // Check for password and password 
                        if (userHash.Equals(dbHash))
                        {
                            Session["LoggedIn"] = tb_email.Text.Trim();

                            // createa a new GUID and save into the session
                            string guid = Guid.NewGuid().ToString();
                            Session["AuthToken"] = guid;

                            // now create a new cookie with this guid value
                            Response.Cookies.Add(new HttpCookie("AuthToken", guid));
                            sendCode();
                            Response.Redirect("~/Auth/EnterOTP.aspx?emailadd=" + tb_email.Text, false);

                        }
                        else
                        {
                            loginMessage.Text = "Wrong username or password";
                        }

                    }

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
                finally { }
            }//if captcha
            else
            {
                loginMessage.Text = "Validate captcha to prove that you are a human.";
            }
        }
        protected string getDBHash(string userid)
        {
            string h = null;
            SqlConnection connection = new SqlConnection(MYDBConnectionString);
            string sql = "select PasswordHash FROM Account WHERE Email=@Email";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Email", userid);
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        if (reader["PasswordHash"] != null)
                        {
                            if (reader["PasswordHash"] != DBNull.Value)
                            {
                                h = reader["PasswordHash"].ToString();
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { connection.Close(); }
            return h;
        }
        protected string getDBSalt(string userid)
        {
            string s = null;
            SqlConnection connection = new SqlConnection(MYDBConnectionString);
            string sql = "select PASSWORDSALT FROM ACCOUNT WHERE Email=@Email";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Email", userid);
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["PASSWORDSALT"] != null)
                        {
                            if (reader["PASSWORDSALT"] != DBNull.Value)
                            {
                                s = reader["PASSWORDSALT"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { connection.Close(); }
            return s;
        }
        private void sendCode()
        {
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential("", "");
            smtp.EnableSsl = true;
            MailMessage msg = new MailMessage();
            msg.Subject = "Here's your OTP code";

            string myquery = "Select * from Account where Email='" + tb_email.Text + "'";
            SqlConnection con = new SqlConnection(MYDBConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = myquery;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                otp = ds.Tables[0].Rows[0]["OTP"].ToString();
            }
            con.Close();
            msg.Body = "Your otp is " + otp + ". Kindly enter this code to login."+"\n\n\nThanks & Regards\nSITConnect";

            string toaddress = tb_email.Text;
            msg.To.Add(toaddress);
            string fromaddress = "SITConnect <dummytrashtest2@gmail.com>";
            msg.From = new MailAddress(fromaddress);
            try
            {
                smtp.Send(msg);
            }
            catch
            {
                throw;
            }
        }

    }
}