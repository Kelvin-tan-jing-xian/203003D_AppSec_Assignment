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

namespace _203003D_AppSec_Assignment
{
    public partial class Register : System.Web.UI.Page
    {
        // Every page must initialise this
        string MYDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MYDBConnection"].ConnectionString;
        static string finalHash;
        static string salt;
        byte[] Key;
        byte[] IV;
        protected void Page_Load(object sender, EventArgs e)
        {

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

            Page.Validate();
            if (Page.IsValid == true)
            {
                // Server side validation
                switch (scores)
                {
                    case 1:
                        status = "Very Weak";
                        break;
                    case 2:
                        status = "Weak";
                        break;
                    case 3:
                        status = "Medium";
                        break;
                    case 4:
                        status = "Strong";
                        break;
                    case 5:
                        status = "Very Strong";
                        break;
                    default:
                        break;
                }
                lbl_pwdchecker.Text = "Status: " + status;
                if (scores < 4)
                {
                    lbl_pwdchecker.ForeColor = Color.Red;
                    return;
                }
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
                Response.Redirect("Login.aspx", false);
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
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Account VALUES(@FirstName, @LastName, @CardName, @CardNumber, @CVV, @CardExpiryDate, @BirthDate, @Email, @PasswordHash, @PasswordSalt, @DateTimeRegistered, @EmailVerified, @Photo, @IV, @Key)"))
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
                            cmd.Parameters.AddWithValue("@EmailVerified", DBNull.Value);
                            cmd.Parameters.AddWithValue("@Photo", tb_photo.Text.Trim());
                            cmd.Parameters.AddWithValue("@IV", Convert.ToBase64String(IV));
                            cmd.Parameters.AddWithValue("@Key", Convert.ToBase64String(Key));

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
                throw new Exception(ex.ToString());
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
    }
}