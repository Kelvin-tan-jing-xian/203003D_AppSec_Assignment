<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="_203003D_AppSec_Assignment.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Form</title>
    <script src="https://www.google.com/recaptcha/api.js?render=6LeaijwdAAAAAHhzNqE504GpPGh_Je6TBSfUnHUS"></script>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 92px;
        }
        .auto-style3 {
            width: 92px;
            height: 29px;
        }
        .auto-style4 {
            height: 29px;
        }
    </style>
</head>
<body>
    <h1>Login</h1>
    <form id="form1" runat="server">
        <div>
                
                
                <table class="auto-style1">
                    <tr>
                        <td class="auto-style2">
                <asp:Label ID="lbl_userid" runat="server" Text="Email"></asp:Label>
                        </td>
                        <td> <asp:TextBox ID="tb_email" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style3">
                <asp:Label ID="lbl_pwd" runat="server" Text="Password"></asp:Label>
                        </td>
                        <td class="auto-style4"><asp:TextBox ID="tb_pwd" TextMode="Password" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style2">&nbsp;</td>
                        <td>
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Registration/ResetPassword.aspx">Forgot Password?</asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style2">&nbsp;</td>
                        <td>
                <asp:Button ID="btn_login" runat="server" OnClick="LoginMe" Text="Login" Width="165px" />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style2">
                            &nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
                <br />
                <asp:Label ID="loginMessage" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label>
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <br />
                <br />
                <input type="hidden" id="g-recaptcha-response" name="g-recaptcha-response"/>
                <br />

        </div>
    </form>
    <script>
        grecaptcha.ready(function () {
            grecaptcha.execute('6LeaijwdAAAAAHhzNqE504GpPGh_Je6TBSfUnHUS', { action: 'Login' }).then(function (token) {
                document.getElementById("g-recaptcha-response").value = token;
            });
        });
    </script>
</body>
</html>
