using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _203003D_AppSec_Assignment.Registration
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
                //if (!IsPasswordResetLinkValid())
                //{
                    //lblMessage.ForeColor = System.Drawing.Color.Red;
                    //lblMessage.Text = "Password Reset link has expired or is invalid";
                //}
            //}
        }

        //protected void btnSave_Click(object sender, EventArgs e)
        //{

        //}
        //private bool ChangeUserPassword()
        //{
            //string pwd = txtNewPassword.Text.ToString().Trim();
            //string userid = tb_email.Text.ToString().Trim();

            //SHA512Managed hashing = new SHA512Managed();

            //List<SqlParameter> paramList = new List<SqlParameter>()
            //{
                //new SqlParameter()
                //{
                    //ParameterName = "@GUID",
                    //Value = Request.QueryString["uid"]
                //},
                //new SqlParameter()
                //{
                    //ParameterName = "@Password",
                    //Value = 
                //}

            //};
            //return ExecuteSP("spChangePassword", paramList);
        //}
        //private bool IsPasswordResetLinkValid()
        //{
            //List<SqlParameter> paramList = new List<SqlParameter>()
            //{
                //new SqlParameter()
                //{
                    //ParameterName = "@GUID",
                    //Value = Request.QueryString["uid"]
                //}
            //};
            //return ExecuteSP("spIsPasswordResetLinkValid", paramList);
        //}
        //private bool ExecuteSP(string SPName, List<SqlParameter> SPParameters)
        //{
            //string CS = ConfigurationManager.ConnectionStrings["MYDBConnection"].ConnectionString;
            //using (SqlConnection con = new SqlConnection(CS))
            //{
                //SqlCommand cmd = new SqlCommand(SPName, con);
                //cmd.CommandType = CommandType.StoredProcedure;
                //foreach(SqlParameter parameter in SPParameters)
                //{
                    //cmd.Parameters.Add(parameter);
                //}
                //con.Open();
                //return Convert.ToBoolean(cmd.ExecuteScalar());
            //}
        //}
    }
}