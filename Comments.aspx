<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Comments.aspx.cs" Inherits="_203003D_AppSec_Assignment.Comments" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <h1>Post Any Comments Here</h1>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Your comments:"></asp:Label>
        </div>
        <asp:TextBox ID="tb_comments" runat="server" Height="81px" Width="271px"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="btn_submit" runat="server" OnClick="btn_submit_Click" Text="Post" />
        <br />
        <br />
        <asp:Label ID="lbl_comments" runat="server" Text="Nothing to display..."></asp:Label>
    </form>
</body>
</html>
