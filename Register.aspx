<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="_203003D_AppSec_Assignment.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registration</title>
        <script type="text/javascript">
        function validate() {
            var str = document.getElementById('<%=tb_pwd.ClientID%>').value;
            if (str.length < 12) {
                document.getElementById("lbl_pwdchecker").innerHTML = "Password Length must be at least 12 characters";
                document.getElementById("lbl_pwdchecker").style.color = "Red";
                return ("too short");
            }
            else if (str.search(/[0-9]/) == -1) {
                document.getElementById("lbl_pwdchecker").innerHTML = "Password require at least 1 number";
                document.getElementById("lbl_pwdchecker").style.color = "Red";
                return ("no_number");
            }
            else if (str.search(/[A-Z]/) == -1) {
                document.getElementById("lbl_pwdchecker").innerHTML = "Password require at least 1 uppercase character";
                document.getElementById("lbl_pwdchecker").style.color = "Red";
                return ("no_uppercase");
            }
            else if (str.search(/[a-z]/) == -1) {
                document.getElementById("lbl_pwdchecker").innerHTML = "Password require at least 1 lowercase character";
                document.getElementById("lbl_pwdchecker").style.color = "Red";
                return ("no_lowercase");
            }
            else if (str.search(/[^A-Za-z0-9]/) == -1) {
                document.getElementById("lbl_pwdchecker").innerHTML = "Password require at least 1 special character";
                document.getElementById("lbl_pwdchecker").style.color = "Red";
                return ("no_specialchar");
            }

            document.getElementById("lbl_pwdchecker").innerHTML = "Excellent!"
            document.getElementById("lbl_pwdchecker").style.color = "Blue";
        }
        </script>

</head>
<body>
    <h1>Registration Form</h1>
    <form id="form1" runat="server" enctype="multipart/form-data">
        <div>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ValidationGroup="AllValidators" />
            <br />
            <asp:Label ID="fname" runat="server" Text="First Name:"></asp:Label>
&nbsp;<asp:TextBox ID="tb_fname" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tb_fname" Display="Dynamic" ErrorMessage="First Name is required" ForeColor="Red" SetFocusOnError="True" ValidationGroup="AllValidators">*</asp:RequiredFieldValidator>
            <br />
            <br />
            <asp:Label ID="lbl_lname" runat="server" Text="Last Name:"></asp:Label>
&nbsp;<asp:TextBox ID="tb_lname" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tb_lname" Display="Dynamic" ErrorMessage="Last Name is required" ForeColor="Red" SetFocusOnError="True" ValidationGroup="AllValidators">*</asp:RequiredFieldValidator>
            <br />
            <br />
            <asp:Label ID="lbl_cardName" runat="server" Text="Name on card:"></asp:Label>
&nbsp;<asp:TextBox ID="tb_cardName" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tb_cardName" Display="Dynamic" ErrorMessage="Card Name is required" ForeColor="Red" SetFocusOnError="True" ValidationGroup="AllValidators">*</asp:RequiredFieldValidator>
            <br />
            <br />
            <asp:Label ID="lbl_cardNumber" runat="server" Text="Card Number:"></asp:Label>
&nbsp;<asp:TextBox ID="tb_cardNumber" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="tb_cardNumber" Display="Dynamic" ErrorMessage="Card Number is required" ForeColor="Red" SetFocusOnError="True" ValidationGroup="AllValidators">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="tb_cardNumber" Display="Dynamic" ErrorMessage="Only numbers are allowed for Card Number" ForeColor="Red" SetFocusOnError="True" ValidationExpression="\b(?:\d[ -]*?){13,16}\b" ValidationGroup="AllValidators">Invalid Card Number</asp:RegularExpressionValidator>
            <br />
            <br />
            <asp:Label ID="lbl_cvv" runat="server" Text="CVV:"></asp:Label>
&nbsp;<asp:TextBox ID="tb_cvv" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="tb_cvv" Display="Dynamic" ErrorMessage="CVV is required" ForeColor="Red" SetFocusOnError="True" ValidationGroup="AllValidators">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="tb_cvv" Display="Dynamic" ErrorMessage="Only numbers are allowed for CVV" ForeColor="Red" SetFocusOnError="True" ValidationExpression="^[0-9]{3,4}$" ValidationGroup="AllValidators">Invalid CVV</asp:RegularExpressionValidator>
            <br />
            <br />
            <asp:Label ID="lbl_expiryDate" runat="server" Text="Card Expiry Date (MM/YY):"></asp:Label>
&nbsp;<asp:TextBox ID="tb_expiryDate" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="tb_expiryDate" Display="Dynamic" ErrorMessage="Card Expiry Date is required" ForeColor="Red" SetFocusOnError="True" ValidationGroup="AllValidators">*</asp:RequiredFieldValidator>
            <br />
            <br />
            <asp:Label ID="lbl_birthDate" runat="server" Text="Birth Date:"></asp:Label>
            &nbsp;<asp:TextBox  ID="tb_birthDate" placeholder="2018-07-22" runat="server"></asp:TextBox>
&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="tb_birthDate" Display="Dynamic" ErrorMessage="Birth Date is required" ForeColor="Red" SetFocusOnError="True" ValidationGroup="AllValidators">*</asp:RequiredFieldValidator>
            <br />
            <br />
            <asp:Label ID="lbl_email" runat="server" Text="Email:"></asp:Label>
&nbsp;<asp:TextBox TextMode="Email" ID="tb_email" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tb_email" Display="Dynamic" ErrorMessage="E-mail address is required." ForeColor="Red" SetFocusOnError="True" ValidationGroup="AllValidators">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="tb_email" Display="Dynamic" ErrorMessage="Incorrect email format" ForeColor="Red" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="AllValidators">Invalid Email</asp:RegularExpressionValidator>
            <br />
            <br />
            <asp:Label ID="lbl_password" runat="server" Text="Password"></asp:Label>
            <%--            onkeyup="javascript:validate()"--%>
            <asp:TextBox ID="tb_pwd" onkeyup="javascript:validate()" TextMode="Password"  runat="server"></asp:TextBox>
            <asp:Label ID="lbl_pwdchecker" runat="server" Text="pwdchecker"></asp:Label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="tb_pwd" Display="Dynamic" ErrorMessage="Password is required" ForeColor="Red" SetFocusOnError="True" ValidationGroup="AllValidators">*</asp:RequiredFieldValidator>
            <br />
            <br />
            <br />
            <asp:Label ID="lbl_photo" runat="server" Text="User Photo:"></asp:Label>
            &nbsp;<asp:TextBox ID="tb_photo" runat="server"></asp:TextBox>

            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="tb_photo" Display="Dynamic" ErrorMessage="Photo is required" ForeColor="Red" SetFocusOnError="True" ValidationGroup="AllValidators">*</asp:RequiredFieldValidator>

            <br />
            <br />
            <asp:Button ID="btn_submit" runat="server" OnClick="btn_submit_Click" Text="Submit" Height="39px" Width="92px" />
        </div>
    </form>
</body>
</html>
