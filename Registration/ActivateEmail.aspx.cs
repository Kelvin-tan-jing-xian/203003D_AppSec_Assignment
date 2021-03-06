using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace _203003D_AppSec_Assignment
{
    public partial class ActivateEmail : System.Web.UI.Page
    {
        string MYDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MYDBConnection"].ConnectionString;
        static string finalHash;
        static string salt;
        byte[] Key;
        byte[] IV;

        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Text = "Your email is " + HttpUtility.HtmlEncode(Request.QueryString["emailadd"].ToString()) + " , Kindly check your mail inbox for activation code";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string myquery = "Select * from Account where Email = @Email";
            SqlConnection con = new SqlConnection(MYDBConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@Email", Request.QueryString["emailadd"]);
            cmd.CommandText = myquery;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string activationcode;
                activationcode = ds.Tables[0].Rows[0]["ActivationCode"].ToString();
                if (activationcode == TextBox1.Text)
                {
                    changestatus();
                    Response.Redirect("~/Auth/Login.aspx", false);
                }
                else
                {
                    Label1.ForeColor = System.Drawing.Color.Red;
                    Label1.Text = "Invalid activation code, please check your inbox and spam folder";
                }
            }
            con.Close();
        }
        private void changestatus()
        {
            string updatedata = "Update Account set EmailVerified = 'Verified' where Email = @Email";
            SqlConnection con = new SqlConnection(MYDBConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@Email", Request.QueryString["emailadd"]);
            cmd.CommandText = updatedata;
            cmd.Connection = con;
            cmd.ExecuteNonQuery();

        }
    }
}