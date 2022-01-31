using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.IO;


namespace _203003D_AppSec_Assignment
{
    public partial class Register : System.Web.UI.Page
    {
        static String activationcode;
        static String otp;
        
        // Every page must initialise this
        string MYDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MYDBConnection"].ConnectionString;
        static string finalHash;
        static string salt;
        byte[] Key;
        byte[] IV;
        protected void Page_Load(object sender, EventArgs e)
        {
            RangeValidator1.MinimumValue = DateTime.Now.AddYears(-120).ToShortDateString();
            RangeValidator1.MaximumValue = DateTime.Now.AddYears(-7).ToShortDateString();
            if (!IsPostBack)
            {
                Calendar1.Visible = false;
            }
        }
        private int checkPassword(string password)
        {
            int score = 0;
            //score 1
            if (password.Length < 12)
            {
                return 1;
            }
            else
            {
                score = 1;
            }
            //score 2
            if (Regex.IsMatch(password, "[a-z]"))
            {
                score++;
            }
            //score 3
            if (Regex.IsMatch(password, "[A-Z]"))
            {
                score++;
            }
            //score 4
            if (Regex.IsMatch(password, "[0-9]"))
            {
                score++;
            }
            //score 5
            if (Regex.IsMatch(password, "[!@#$%^&*]"))
            {
                score++;
            }

            return score;
        }


        protected void btn_submit_Click(object sender, EventArgs e)
        {
            int scores = checkPassword(tb_pwd.Text);
            string status = "";
            HttpPostedFile postedFile = FileUpload1.PostedFile;
            string fileName = Path.GetFileName(postedFile.FileName);
            string fileExtension = Path.GetExtension(fileName);
            Page.Validate();
            if (Page.IsValid == true)
            {
                if (checkemail() == true)
                {
                    Label1.Text = "Invalid email";

                }
                else
                {
                    if (fileExtension.ToLower() == ".jpg" || fileExtension.ToLower() == ".bmp" || fileExtension.ToLower() == ".gif" || fileExtension.ToLower() == ".png")
                    {
                        // Server side validation for password
                        switch (scores)
                        {
                            case 1:
                                status = "Very Weak, please strengthen your password";
                                break;
                            case 2:
                                status = "Weak, please strengthen your password";
                                break;
                            case 3:
                                status = "Medium, please strengthen your password";
                                break;
                            case 4:
                                status = "Strong, but can be better";
                                break;
                            case 5:
                                status = "Excellent";
                                break;
                            default:
                                break;
                        }
                        lbl_pwdchecker.Text = "Status: " + status;
                        if (scores < 5)
                        {
                            lbl_pwdchecker.ForeColor = Color.Red;
                            return;
                        }
                        // continue on with processing
                        lbl_pwdchecker.ForeColor = Color.Green;

                        string pwd = tb_pwd.Text.ToString().Trim();
                        //Generate random "salt"
                        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                        byte[] saltByte = new byte[8];
                        //Fills array of bytes with a cryptographically strong sequence of random values.
                        rng.GetBytes(saltByte);
                        salt = Convert.ToBase64String(saltByte);
                        SHA512Managed hashing = new SHA512Managed();
                        string pwdWithSalt = pwd + salt;
                        byte[] plainHash = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwd));
                        byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));
                        finalHash = Convert.ToBase64String(hashWithSalt);
                        RijndaelManaged cipher = new RijndaelManaged();
                        cipher.GenerateKey();
                        Key = cipher.Key;
                        IV = cipher.IV;
                        createAccount();
                        sendCode();
                        Response.Redirect("ActivateEmail.aspx?emailadd=" + tb_email.Text);

                    }
                    else
                    {
                        Label2.Text = "Only images (.jpg, .png, .gif and .bmp) can be uploaded";
                    }


                }

            }
            else
            {
                   return;
            }
        }
        protected void createAccount()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(MYDBConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Account VALUES(@FirstName, @LastName, @CardName, @CardNumber, @CVV, @CardExpiryDate, @BirthDate, @Email, @PasswordHash, @PasswordSalt, @DateTimeRegistered, @EmailVerified, @Photo, @IV, @Key, @ActivationCode, @role)"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@Email", tb_email.Text.Trim());
                            cmd.Parameters.AddWithValue("@FirstName", tb_fname.Text.Trim());
                            cmd.Parameters.AddWithValue("@LastName", tb_lname.Text.Trim());
                            cmd.Parameters.AddWithValue("@CardName", Convert.ToBase64String(encryptData(tb_cardName.Text.Trim())));
                            cmd.Parameters.AddWithValue("@CardNumber", Convert.ToBase64String(encryptData(tb_cardNumber.Text.Trim())));
                            cmd.Parameters.AddWithValue("@CVV", Convert.ToBase64String(encryptData(tb_cvv.Text.Trim())));
                            cmd.Parameters.AddWithValue("@CardExpiryDate", Convert.ToBase64String(encryptData(tb_expiryDate.Text.Trim())));
                            //cmd.Parameters.AddWithValue("@CardNumber", tb_cardNumber.Text.Trim());
                            //cmd.Parameters.AddWithValue("@CVV", tb_cvv.Text.Trim());
                            //cmd.Parameters.AddWithValue("@CardExpiryDate", tb_expiryDate.Text.Trim());
                            cmd.Parameters.AddWithValue("@BirthDate", tb_birthDate.Text.Trim());
                            cmd.Parameters.AddWithValue("@PasswordHash", finalHash);
                            cmd.Parameters.AddWithValue("@PasswordSalt", salt);
                            cmd.Parameters.AddWithValue("@DateTimeRegistered", DateTime.Now);
                            cmd.Parameters.AddWithValue("@EmailVerified", "Unverified");

                            FileUpload1.SaveAs(Server.MapPath("~/Photos/") + Path.GetFileName(FileUpload1.FileName));
                            string link = "Photos/" + Path.GetFileName(FileUpload1.FileName);
                            cmd.Parameters.AddWithValue("@Photo", link);

                            cmd.Parameters.AddWithValue("@IV", Convert.ToBase64String(IV));
                            cmd.Parameters.AddWithValue("@Key", Convert.ToBase64String(Key));

                            Random random = new Random();
                            activationcode = random.Next(1001, 9999).ToString();
                            cmd.Parameters.AddWithValue("@ActivationCode", activationcode);

                            if (rbtn_student.Checked)
                            {
                                cmd.Parameters.AddWithValue("@role", rbtn_student.Text.Trim());
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@role", rbtn_staff.Text.Trim());
                            }


                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                //throw new Exception(ex.ToString());
                Label3.Text = "Sorry, something went wrong.";
            }
        }
        private void sendCode()
        {
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential("", "");
            smtp.EnableSsl = true;
            MailMessage msg = new MailMessage();
            msg.Subject = "Verify your email address";
            msg.Body = "Dear " + HttpUtility.HtmlEncode(tb_fname.Text) + ", Your activation code is " + activationcode + "\n\n\nThanks & Regards\nSITConnect";
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

        private Boolean checkemail()
        {
            Boolean emailavailable = false;
            using (SqlConnection con = new SqlConnection(MYDBConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Select * from Account where Email= @Email", con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Email", tb_email.Text.Trim());
                        sda.SelectCommand = cmd;
                        DataSet ds = new DataSet();
                        sda.Fill(ds);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            emailavailable = true;
                        }
                        con.Close();
                        return emailavailable;
                    }
                }
            }
        }

        protected byte[] encryptData(string data)
        {
            byte[] cipherText = null;
            try
            {
                RijndaelManaged cipher = new RijndaelManaged();
                cipher.IV = IV;
                cipher.Key = Key;
                ICryptoTransform encryptTransform = cipher.CreateEncryptor();
                byte[] plainText = Encoding.UTF8.GetBytes(data);
                cipherText = encryptTransform.TransformFinalBlock(plainText, 0, plainText.Length);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { }
            return cipherText;
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            if (Calendar1.Visible)
            {
                Calendar1.Visible = false;

            }
            else
            {
                Calendar1.Visible = true;

            }
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            tb_birthDate.Text = Calendar1.SelectedDate.ToShortDateString();
            Calendar1.Visible = false;
        }

        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.IsOtherMonth)
            {
                e.Day.IsSelectable = false;
                e.Cell.BackColor = Color.Red;
            }
        }

    }
}