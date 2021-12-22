<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageUsers.aspx.cs" Inherits="_203003D_AppSec_Assignment.ManageUsers" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
         <title>SQL InJection</title>
    </head>
    <body>
         <form id="form1" runat="server">

             <div style="width: 50%; margin: 0 auto; text-align: center;">
                 <table>
                     <tr>
                         <td colspan="2">
                             <h2>SQL Injection Demo</h2>
                         </td>
                     </tr>
                     <tr>
                         <td>
                             Search by userid <asp:textbox id="txtUserID" runat="server"></asp:textbox>
                             <asp:button id="btnSubmit" onclick="BtnSubmit_Click" runat="server" text="Search" />
                             <asp:Label ID="lbl_error" runat="server"></asp:Label>
                         </td>
                         <td>
                             &nbsp;</td>
                     </tr>
                     <tr>
                         <asp:gridview id="gvUserInfo" width="109%" runat="server" datakeynames="Id" autogeneratecolumns="false" style="margin-right: 0px">
                             <Columns>
                                 <asp:BoundField DataField="Id" HeaderText="Id" />
                                 <asp:BoundField DataField="FirstName" HeaderText="FirstName" />
                                 <asp:BoundField DataField="LastName" HeaderText="LastName" />
                                 <asp:BoundField DataField="Email" HeaderText="Email" />
                                 <asp:HyperLinkField DataNavigateUrlFields="Id" DataNavigateUrlFormatString="viewuser.aspx?id={0}" Text="View User" HeaderText="action" />
                             </Columns>
                         </asp:gridview>
                     </tr>
                 </table>
             </div>
         </form>
    </body>
</html>