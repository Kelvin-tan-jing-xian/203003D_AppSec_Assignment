using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace _203003D_AppSec_Assignment
{
    public partial class ManageUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (getRole() != "admin")
                {
                    Response.Redirect("~/CustomError/HTTP403.aspx", false);
                }

                DataSet dset = new DataSet();
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MYDBConnection"].ToString()); // MYDBConnectionString
                using (conn)
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    SqlCommand cmd = new SqlCommand("SELECT Id, FirstName, LastName, Email, Photo FROM Account", conn);
                    cmd.CommandType = CommandType.Text;
                    adapter.SelectCommand = cmd;
                    adapter.Fill(dset);
                    gvUserInfo.DataSource = dset;
                    gvUserInfo.DataBind();
                }
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet dset = new DataSet();
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MYDBConnection"].ToString());
                using (conn)
                {
                    if (txtUserID.Text != string.Empty)
                    {
                        conn.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        string sqlQuery = string.Format("SELECT Id, FirstName, LastName, Email, Photo FROM Account WHERE Id =@0");
                        SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@0", txtUserID.Text);
                        adapter.SelectCommand = cmd;
                        adapter.Fill(dset);
                        gvUserInfo.DataSource = dset;
                        gvUserInfo.DataBind();
                    }
                }

            }
            catch(SqlException ex) {
                // Include a label to present some comments to the user
                lbl_error.Text = "Invalid search input!";
                lbl_error.ForeColor = System.Drawing.Color.Red;
            }


        }
        private string getRole()
        {

            List<SqlParameter> paramList = new List<SqlParameter>()
            {
                new SqlParameter()
                {
                    ParameterName = "@Email",
                    Value = Session["LoggedIn"].ToString()
                },

            };
            return ExecuteSP("spGetRole", paramList);
        }
        private string ExecuteSP(string SPName, List<SqlParameter> SPParameters)
        {
            string CS = ConfigurationManager.ConnectionStrings["MYDBConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand(SPName, con);
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter parameter in SPParameters)
                {
                    cmd.Parameters.Add(parameter);
                }
                con.Open();
                return Convert.ToString(cmd.ExecuteScalar());
            }
        }

    }
}