<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="_203003D_AppSec_Assignment.HomePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 196px;
        }
        .auto-style3 {
            width: 181px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <fieldset>
                <legend>HomePage</legend>
                <br />
                <asp:Label ID="lblMessage" runat="server" EnableViewState="false" />

                <br />
                <table class="auto-style1">
                    <tr>
                        <td class="auto-style2">
                            <asp:Label ID="lbl_currentpwd" runat="server" Text="Current Password"></asp:Label>
                        </td>
                        <td class="auto-style3">
                            <asp:TextBox ID="tb_currentpwd" runat="server" TextMode="Password"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorCurrentPwd" runat="server" ControlToValidate="tb_currentpwd" Display="Dynamic" ErrorMessage="Current password is required" ForeColor="Red" SetFocusOnError="True" ValidationGroup="AllValidators">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style2">
                            <asp:Label ID="lbl_newpwd" runat="server" Text="New Password"></asp:Label>
                        </td>
                        <td class="auto-style3">
                            <asp:TextBox ID="tb_newpwd" runat="server" TextMode="Password"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorNewPwd" runat="server" ControlToValidate="tb_newpwd" Display="Dynamic" ErrorMessage="New Password is required" ForeColor="Red" SetFocusOnError="True" ValidationGroup="AllValidators">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style2">
                            <asp:Label ID="lbl_cfmNewPwd" runat="server" Text="Confirm New Password"></asp:Label>
                        </td>
                        <td class="auto-style3">
                            <asp:TextBox ID="tb_cfmNewPwd" runat="server" TextMode="Password"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorCfmNewPwd" runat="server" ControlToValidate="tb_cfmNewPwd" Display="Dynamic" ErrorMessage="Confirm new password is required" ForeColor="Red" SetFocusOnError="True" ValidationGroup="AllValidators">*</asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidatorPwd" runat="server" ControlToCompare="tb_newpwd" ControlToValidate="tb_cfmNewPwd" Display="Dynamic" ErrorMessage="Passwords do not match" ForeColor="Red" SetFocusOnError="True" ValidationGroup="AllValidators">Passwords do not match</asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style2">&nbsp;</td>
                        <td class="auto-style3">
                            <asp:Button ID="btn_changePwd" runat="server" OnClick="btn_changePwd_Click" Text="Change Password" />
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
                <br />
                <asp:Label ID="errorMessage" runat="server" ForeColor="Red"></asp:Label>

                <br />
                <br />
                <asp:Button ID="btnLogout" runat="server" Text="Logout" OnClick="LogoutMe" Visible="false" EnableViewState="false" />

            </fieldset>

        </div>
    </form>
</body>
</html>
