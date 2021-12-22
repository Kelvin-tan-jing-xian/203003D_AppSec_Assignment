<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="_203003D_AppSec_Assignment.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Form</title>
    <script src="https://www.google.com/recaptcha/api.js?render=6LeaijwdAAAAAHhzNqE504GpPGh_Je6TBSfUnHUS"></script>
</head>
<body>
    <h1>Login</h1>
    <form id="form1" runat="server">
        <div>
                <asp:Label ID="lbl_userid" runat="server" Text="Email:"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="tb_email" runat="server"></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="lbl_pwd" runat="server" Text="Password:"></asp:Label>
                &nbsp;<asp:TextBox ID="tb_pwd" runat="server"></asp:TextBox>
                <br />
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btn_login" runat="server" OnClick="LoginMe" Text="Login" Width="165px" />
                <br />
                <br />
                <input type="hidden" id="g-recaptcha-response" name="g-recaptcha-response"/>
                <asp:Label ID="loginMessage" runat="server" EnableViewState="false" Text=""></asp:Label>
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
