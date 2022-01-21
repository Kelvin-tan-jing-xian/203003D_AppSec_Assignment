<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="_203003D_AppSec_Assignment.Registration.ChangePassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 201px;
        }
    </style>
</head>
<body>
    <h1>Change Password</h1>
    <form id="form1" runat="server">
        <br />
    <table class="auto-style1">
        <tr>
            <td class="auto-style2">
        <asp:Label ID="Label1" runat="server" Text="New Password"></asp:Label>
            </td>
            <td>
        <asp:TextBox ID="txtNewPassword" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNewPassword" ErrorMessage="New Password field is required" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">
        <asp:Label ID="Label2" runat="server" Text="Confirm New Password"></asp:Label>
            </td>
            <td>
        <asp:TextBox ID="txtConfirmNewPassword" runat="server"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Passwords do not match" ControlToCompare="txtNewPassword" ControlToValidate="txtConfirmNewPassword" ForeColor="Red" SetFocusOnError="True">*</asp:CompareValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtConfirmNewPassword" ErrorMessage="Confirm New Password field is required" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td>
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
            </td>
        </tr>
        <tr>
            <td class="auto-style2">
                <asp:Label ID="lblMessage" runat="server" ></asp:Label>
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" />
    </form>
    </body>
</html>
