<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="_203003D_AppSec_Assignment.ResetPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            margin-left: 0px;
        }
        .auto-style3 {
            width: 104px;
        }
    </style>
</head>
<body>
    <h1>Enter your first name to reset your password</h1>
    <form id="form1" runat="server">
        <div>
            &nbsp;&nbsp;<table class="auto-style1">
                <tr>
                    <td class="auto-style3">
            <asp:Label ID="Label1" runat="server" Text="First Name"></asp:Label>
                    </td>
                    <td>
            <asp:TextBox ID="TextBox1" runat="server" CssClass="auto-style2"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">&nbsp;</td>
                    <td>
            <asp:Button ID="Button1" runat="server" Text="Reset Password" Width="164px" OnClick="Button1_Click" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </div>
        <p>
            <asp:Label ID="Label2" runat="server" ForeColor="Red" Text=""></asp:Label>
        </p>
    </form>
</body>
</html>
