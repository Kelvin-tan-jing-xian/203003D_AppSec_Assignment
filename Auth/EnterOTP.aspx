<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EnterOTP.aspx.cs" Inherits="_203003D_AppSec_Assignment.Auth.EnterOTP" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 43%;
            height: 60px;
        }
        .auto-style2 {
            height: 26px;
        }
        .auto-style3 {
            height: 26px;
            width: 133px;
        }
        .auto-style4 {
            width: 133px;
        }
        .auto-style5 {
            margin-left: 0px;
        }
    </style>
</head>
<body>
    <h1>Enter OTP here</h1>
    <br />
    <asp:Label ID="lbl_display" runat="server"></asp:Label>
    <br />

    <form id="form1" runat="server">
        <table class="auto-style1">
            <tr>
                <td class="auto-style3">Enter your OTP</td>
                <td class="auto-style2">
                    <asp:TextBox ID="TextBox1" TextMode="Password" runat="server" Width="155px" CssClass="auto-style5"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style4">&nbsp;</td>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="Login" OnClick="Button1_Click" Width="160px" />
                </td>
            </tr>
        </table>
        <asp:Label ID="lbl_error" runat="server" Text=""></asp:Label>
    </form>
</body>
</html>
