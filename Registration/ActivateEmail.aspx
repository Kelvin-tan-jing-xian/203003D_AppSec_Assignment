<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActivateEmail.aspx.cs" Inherits="_203003D_AppSec_Assignment.ActivateEmail" %>

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
            width: 210px;
        }
        .auto-style4 {
            width: 210px;
        }
    </style>
</head>
<body>
    <h1>Verify Your Email Address</h1>
    <form id="form1" runat="server">
        <table class="auto-style1">
            <tr>
                <td class="auto-style3">Enter your activation code</td>
                <td class="auto-style2">
                    <asp:TextBox ID="TextBox1" runat="server" Width="155px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style4">&nbsp;</td>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="Verify Email" OnClick="Button1_Click" />
                </td>
            </tr>
        </table>
        <asp:Label ID="Label1" runat="server"></asp:Label>
    </form>
</body>
</html>
