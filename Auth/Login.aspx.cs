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
        static String randomNumber;

        public class MyObject
        {
            public string success { get; set; }
            public List<string> ErrorMessage { get; set; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["invalidloginattempt"] = null;
            }
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
                    SqlConnection scon = new SqlConnection(MYDBConnectionString);
                    SqlCommand cmd = new SqlCommand("Select Email, PasswordHash, locked, lockdatetime from Account where Email = @Email");
                    cmd.Parameters.AddWithValue("@Email", userid);
                    cmd.Connection = scon;
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    bool lockstatus;
                    DateTime locktimedate = DateTime.Now;

                    if (dbSalt != null && dbSalt.Length > 0 && dbHash != null && dbHash.Length > 0)
                    {
                        string pwdWithSalt = pwd + dbSalt;
                        byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));
                        string userHash = Convert.ToBase64String(hashWithSalt);
                        string uname = ds.Tables[0].Rows[0]["Email"].ToString();
                        string pass = ds.Tables[0].Rows[0]["PasswordHash"].ToString();
                        lockstatus = Convert.ToBoolean(ds.Tables[0].Rows[0]["locked"].ToString());
                        if (lockstatus == true)
                        {
                            locktimedate = Convert.ToDateTime(ds.Tables[0].Rows[0]["lockdatetime"].ToString());
                            locktimedate = Convert.ToDateTime(locktimedate.ToString("dd/MM/yyyy HH:mm:ss"));
                        }
                        scon.Close();
                        if (lockstatus == true)
                        {
                            DateTime cdatetime = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                            TimeSpan ts = cdatetime.Subtract(locktimedate);
                            Int32 minuteslocked = Convert.ToInt32(ts.TotalMinutes);
                            Int32 pendingminutes = 1 - minuteslocked;
                            if (pendingminutes <= 0)
                            {
                                unlockaccount(userid);
                            }
                            else
                            {
                                loginMessage.Text = "Your account has been locked for 1 minute due to three invalid attempts. Your account will be unlocked automatically after 1 minute";
                            }

                        }
                        else
                        {

                            // Check for password and password 
                            if (userHash.Equals(dbHash))
                            {
                                Session["LoggedIn"] = tb_email.Text.Trim();
                                // createa a new GUID and save into the session
                                string guid = Guid.NewGuid().ToString();
                                Session["AuthToken"] = guid;

                                // now create a new cookie with this guid value
                                Response.Cookies.Add(new HttpCookie("AuthToken", guid));
                                InsertOTPinDB();
                                sendCode();
                                Response.Redirect("~/Auth/EnterOTP.aspx?emailadd=" + tb_email.Text, false);

                            }
                            else
                            {
                                int attemptcount;
                                if (Session["invalidloginattempt"] != null)
                                {
                                    attemptcount = Convert.ToInt16(Session["invalidloginattempt"].ToString());
                                    attemptcount = attemptcount + 1;

                                }
                                else
                                {
                                    attemptcount = 1;
                                }
                                Session["invalidloginattempt"] = attemptcount;
                                if (attemptcount == 3)
                                {
                                    loginMessage.Text = "Your account has been locked for 1 minute for three invalid attempts. Your account will be unlocked automatically after 1 minute";
                                    changelockstatus(userid);
                                }
                                else
                                {
                                    loginMessage.Text = "Invalid Username or Password. You have " + (3 - attemptcount) + " remaining attempts to login";
                                }
                            }
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
        void changelockstatus(string username)
        {
            SqlConnection con = new SqlConnection(MYDBConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("Update Account set locked = 1, lockdatetime = @lockdatetime where Email = @Email");
            cmd.Parameters.AddWithValue("@lockdatetime", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            cmd.Parameters.AddWithValue("@Email", username);
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
        }
        void unlockaccount(string username)
        {
            SqlConnection con = new SqlConnection(MYDBConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("Update Account set locked = 0, lockdatetime = NULL where Email = @Email");
            cmd.Parameters.AddWithValue("@Email", username);
            cmd.Connection = con;
            cmd.ExecuteNonQuery();

        }
        public void InsertOTPinDB()
        {
            SqlConnection con = new SqlConnection(MYDBConnectionString);
            SqlCommand cmd = new SqlCommand("INSERT INTO OTPHistoryTbl (Email,OTP,ctime_Stamp,status) VALUES (@Email,@OTP,@ctime_Stamp,@status) ", con);
            con.Open();
            cmd.Parameters.AddWithValue("@Email", tb_email.Text.ToString());

            Random rnd = new Random();
            randomNumber = (rnd.Next(100000, 999999)).ToString();
            cmd.Parameters.AddWithValue("@OTP", randomNumber);

            cmd.Parameters.AddWithValue("@ctime_Stamp", DateTime.Now.ToString());
            cmd.Parameters.AddWithValue("@status", "0");
            cmd.ExecuteNonQuery();
            con.Close();
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

            SqlConnection con = new SqlConnection(MYDBConnectionString);
            SqlCommand cmd = new SqlCommand("Select * from OTPHistoryTbl where ctime_Stamp=@ctime_Stamp", con);
            cmd.Parameters.AddWithValue("@ctime_Stamp", DateTime.Now.ToString());

            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                randomNumber = ds.Tables[0].Rows[0]["OTP"].ToString();
            }
            con.Close();
            msg.Body = "Your otp is " + randomNumber + ". Kindly enter this code to login."+"\n\n\nThanks & Regards\nSITConnect";

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