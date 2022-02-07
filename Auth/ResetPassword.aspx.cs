using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _203003D_AppSec_Assignment
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string CS = ConfigurationManager.ConnectionStrings["MYDBConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("spResetPassword", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter paramFirstname = new SqlParameter("@FirstName", TextBox1.Text);
                cmd.Parameters.Add(paramFirstname);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    if (Convert.ToBoolean(rdr["ReturnCode"]))
                    {
                        SendPasswordResetEmail(rdr["Email"].ToString(), TextBox1.Text, rdr["UniqueId"].ToString());
                        Label2.Text = "An email with instructions to reset your password is sent to " + rdr["Email"].ToString();

                    }
                    else
                    {
                        Label2.ForeColor = System.Drawing.Color.Red;
                        Label2.Text = "Invalid First Name. If you have not registered an account yet, please head to the registration page";
                    }
                }
            }
        }
        private void SendPasswordResetEmail(string ToEmail, string UserName, string UniqueId)
        {
            MailMessage mailMessage = new MailMessage("dummytrashtest2@gmail.com", ToEmail);

            StringBuilder sbEmailBody = new StringBuilder();
            sbEmailBody.Append("Dear " + UserName + ",<br/><br/>");
            sbEmailBody.Append("Please click on the following link to reset your password");
            sbEmailBody.Append("<br/>");
            sbEmailBody.Append("https://localhost:44378/Auth/ChangePassword.aspx?uid=" + UniqueId);
            sbEmailBody.Append("<br/><br/>");
            sbEmailBody.Append("<b>SITConnect</b>");

            mailMessage.IsBodyHtml = true;

            mailMessage.Body = sbEmailBody.ToString();
            mailMessage.Subject = "Reset Your Password";
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.Credentials = new System.Net.NetworkCredential()
            {
                UserName = "",
                Password = ""
            };
            smtpClient.EnableSsl = true;
            smtpClient.Send(mailMessage);

        }
    }
}