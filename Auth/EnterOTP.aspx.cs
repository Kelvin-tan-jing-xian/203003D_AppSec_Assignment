using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _203003D_AppSec_Assignment.Auth
{
    public partial class EnterOTP : System.Web.UI.Page
    {
        string MYDBConnectionString = ConfigurationManager.ConnectionStrings["MYDBConnection"].ConnectionString;
        static string finalHash;
        static string salt;
        byte[] Key;
        byte[] IV;
        static string randomNumber;
        protected void Page_Load(object sender, EventArgs e)
        {
            lbl_display.Text = "Your email is " + HttpUtility.HtmlEncode(Request.QueryString["emailadd"].ToString()) + " , Kindly check your mail inbox for OTP code";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {


            using (SqlConnection con = new SqlConnection(MYDBConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM OTPHistoryTbl WHERE OTP=@OTP", con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Parameters.AddWithValue("@OTP", TextBox1.Text.Trim());
                        sda.SelectCommand = cmd;
                        DataSet ds1 = new DataSet();
                        sda.Fill(ds1);
                        DateTime OtpCrtDate = Convert.ToDateTime(ds1.Tables[0].Rows[0]["ctime_Stamp"].ToString());

                        SqlCommand cmd2 = new SqlCommand("Select * from OTPHistoryTbl where Email=@Email", con);
                        cmd2.Parameters.AddWithValue("@Email", Request.QueryString["emailadd"].ToString());
                        SqlDataAdapter sda2 = new SqlDataAdapter();
                        sda2.SelectCommand = cmd2;
                        DataSet ds2 = new DataSet();
                        sda2.Fill(ds2);
                        if (ds2.Tables[0].Rows.Count > 0)
                        {
                            randomNumber = ds2.Tables[0].Rows[0]["OTP"].ToString();
                            if (TextBox1.Text != randomNumber)
                            {
                                TimeSpan timeSub = DateTime.Now - OtpCrtDate;
                                if (timeSub.TotalSeconds < 60)
                                {
                                    changeStatus();
                                    Response.Redirect("~/HomePage.aspx", false);

                                }
                                else
                                {
                                    lbl_error.ForeColor = Color.Red;
                                    lbl_error.Text = "Sorry but your OTP is very old. Get a new one";
                                }
                            }
                            else
                            {
                                lbl_error.ForeColor = Color.Red;
                                lbl_error.Text = "Sorry, Your OTP is Invalid. Try again, please.";
                            }

                        }
                        con.Close();

                        

                    }
                }
            }

        }
        private void changeStatus()
        {


            SqlConnection con = new SqlConnection(MYDBConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE OTPHistoryTbl set status='1' where OTP=@OTP", con);
            cmd.Parameters.AddWithValue("@OTP", TextBox1.Text.Trim());
            cmd.Connection = con;
            cmd.ExecuteNonQuery();

        }

    }
}