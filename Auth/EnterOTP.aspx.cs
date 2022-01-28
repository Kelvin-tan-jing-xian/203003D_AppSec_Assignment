using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _203003D_AppSec_Assignment.Auth
{
    public partial class EnterOTP : System.Web.UI.Page
    {
        string MYDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MYDBConnection"].ConnectionString;
        static string finalHash;
        static string salt;
        byte[] Key;
        byte[] IV;

        protected void Page_Load(object sender, EventArgs e)
        {
            lbl_display.Text = "Your email is " + Request.QueryString["emailadd"].ToString() + " , Kindly check your mail inbox for OTP code";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string myquery = "Select * from Account where Email='" + Request.QueryString["emailadd"] + "'";
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
                string dbOtp = ds.Tables[0].Rows[0]["OTP"].ToString();
                if (dbOtp == TextBox1.Text)
                {
                    Response.Redirect("~/HomePage.aspx", false);
                }
                else
                {
                    lbl_error.ForeColor = System.Drawing.Color.Red;
                    lbl_error.Text = "Invalid OTP code, please check your inbox and spam folder";
                }
            }
            con.Close();
        }
    }
}