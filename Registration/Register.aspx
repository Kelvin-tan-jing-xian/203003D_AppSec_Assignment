<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" ValidateRequest="false" Inherits="_203003D_AppSec_Assignment.Register" %>
<!--ValidateRequest="false"-->
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

    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 232px;
        }
        .auto-style3 {
            width: 153px;
        }
        .auto-style4 {
            width: 232px;
            height: 48px;
        }
        .auto-style6 {
            height: 48px;
        }
        .auto-style7 {
            width: 232px;
            height: 29px;
        }
        .auto-style8 {
            width: 153px;
            height: 29px;
        }
        .auto-style9 {
            height: 29px;
        }
    </style>

</head>
<body>
    <h1>Registration Form</h1>
    <form id="form1" runat="server" enctype="multipart/form-data">
        <div>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ValidationGroup="AllValidators" />
            <table class="auto-style1">
                <tr>
                    <td class="auto-style2">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="fname" runat="server" Text="First Name"></asp:Label>
                    </td>
                    <td class="auto-style3"><asp:TextBox ID="tb_fname" runat="server"></asp:TextBox>
                    </td>
                    <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tb_fname" Display="Dynamic" ErrorMessage="First Name is required" ForeColor="Red" SetFocusOnError="True" ValidationGroup="AllValidators">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style7">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lbl_lname" runat="server" Text="Last Name"></asp:Label>
                    </td>
                    <td class="auto-style8"><asp:TextBox ID="tb_lname" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style9">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tb_lname" Display="Dynamic" ErrorMessage="Last Name is required" ForeColor="Red" SetFocusOnError="True" ValidationGroup="AllValidators">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lbl_cardName" runat="server" Text="Name on card"></asp:Label>
                    </td>
                    <td class="auto-style3"><asp:TextBox ID="tb_cardName" runat="server" ></asp:TextBox>
                    </td>
                    <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tb_cardName" Display="Dynamic" ErrorMessage="Card Name is required" ForeColor="Red" SetFocusOnError="True" ValidationGroup="AllValidators">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lbl_cardNumber" runat="server" Text="Card Number"></asp:Label>
                    </td>
                    <td class="auto-style3"><asp:TextBox ID="tb_cardNumber" runat="server"></asp:TextBox>
                    </td>
                    <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="tb_cardNumber" Display="Dynamic" ErrorMessage="Card Number is required" ForeColor="Red" SetFocusOnError="True" ValidationGroup="AllValidators">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="tb_cardNumber" Display="Dynamic" ErrorMessage="Only 13 to 16 digit numbers are allowed for Card Number" ForeColor="Red" SetFocusOnError="True" ValidationExpression="\b(?:\d[ -]*?){13,16}\b" ValidationGroup="AllValidators">Invalid Card Number</asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lbl_cvv" runat="server" Text="CVV"></asp:Label>
                    </td>
                    <td class="auto-style3"><asp:TextBox ID="tb_cvv" runat="server"></asp:TextBox>
                    </td>
                    <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="tb_cvv" Display="Dynamic" ErrorMessage="CVV is required" ForeColor="Red" SetFocusOnError="True" ValidationGroup="AllValidators">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="tb_cvv" Display="Dynamic" ErrorMessage="Only 3 to 4 digit numbers are allowed for CVV" ForeColor="Red" SetFocusOnError="True" ValidationExpression="^[0-9]{3,4}$" ValidationGroup="AllValidators">Invalid CVV</asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">
            <asp:Label ID="lbl_expiryDate" runat="server" Text="Card Expiry Date (MM/YY)"></asp:Label>
                    </td>
                    <td class="auto-style3"><asp:TextBox ID="tb_expiryDate" runat="server"></asp:TextBox>
                    </td>
                    <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="tb_expiryDate" Display="Dynamic" ErrorMessage="Card Expiry Date is required" ForeColor="Red" SetFocusOnError="True" ValidationGroup="AllValidators">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lbl_birthDate" runat="server" Text="Birth Date (dd-mm-yyyy)"></asp:Label>
                    </td>
                    <td class="auto-style3"><asp:TextBox  ID="tb_birthDate" runat="server"></asp:TextBox>
                        <asp:ImageButton ID="ImageButton1" runat="server" Height="41px" ImageUrl="~/Photos/calendar.jpg" OnClick="ImageButton1_Click" Width="48px" />
                        <asp:Calendar ID="Calendar1" runat="server" BackColor="#FFFFCC" BorderColor="#FFCC66" BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#663399" Height="200px" OnDayRender="Calendar1_DayRender" OnSelectionChanged="Calendar1_SelectionChanged" ShowGridLines="True" Width="220px">
                            <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" />
                            <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                            <OtherMonthDayStyle ForeColor="#CC9966" />
                            <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                            <SelectorStyle BackColor="#FFCC66" />
                            <TitleStyle BackColor="#990000" Font-Bold="True" Font-Size="9pt" ForeColor="#FFFFCC" />
                            <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
                        </asp:Calendar>
                    </td>
                    <td><asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="tb_birthDate" Display="Dynamic" ErrorMessage="Birth Date is required" ForeColor="Red" SetFocusOnError="True" ValidationGroup="AllValidators">*</asp:RequiredFieldValidator>
            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="tb_birthDate" Display="Dynamic" ErrorMessage="Age must be between 7 and 120" ForeColor="Red" SetFocusOnError="True" Type="Date" ValidationGroup="AllValidators">Invalid Birth Date</asp:RangeValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lbl_email" runat="server" Text="Email"></asp:Label>
                    </td>
                    <td class="auto-style3"><asp:TextBox TextMode="Email" ID="tb_email" runat="server"></asp:TextBox>
                    </td>
                    <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tb_email" Display="Dynamic" ErrorMessage="E-mail address is required." ForeColor="Red" SetFocusOnError="True" ValidationGroup="AllValidators">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="tb_email" Display="Dynamic" ErrorMessage="Incorrect email format" ForeColor="Red" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="AllValidators">Invalid Email</asp:RegularExpressionValidator>
                        <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lbl_password" runat="server" Text="Password"></asp:Label>
                        </td>
                    <td class="auto-style3">
            <asp:TextBox ID="tb_pwd" onkeyup="javascript:validate()" TextMode="Password"  runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style6">
            <asp:Label ID="lbl_pwdchecker" runat="server"></asp:Label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="tb_pwd" Display="Dynamic" ErrorMessage="Password is required" ForeColor="Red" SetFocusOnError="True" ValidationGroup="AllValidators">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Label6" runat="server" Text="Confirm password"></asp:Label>
                    </td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_cfmpwd" TextMode="Password" runat="server"></asp:TextBox>

                    </td>
                    <td>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorCfmpwd" runat="server" ControlToValidate="tb_cfmpwd" ErrorMessage="Confirm password is required" ForeColor="Red" SetFocusOnError="True" Display="Dynamic" ValidationGroup="AllValidators">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidatorCfmpwd" runat="server" ControlToCompare="tb_pwd" ControlToValidate="tb_cfmpwd" ErrorMessage="Passwords do not match" ForeColor="Red" SetFocusOnError="True" Display="Dynamic">Passwords do not match</asp:CompareValidator>

                    </td>
                </tr>
                                
                <tr>
                    <td class="auto-style2">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lbl_photo" runat="server" Text="User Photo"></asp:Label>
                    </td>
                    <td class="auto-style3">
                        

                        <asp:FileUpload ID="FileUpload1" runat="server" Width="276px" accept=".png, .jpg, .jpeg, .bmp, .gif"/>

                    </td>
                    <td>

            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="FileUpload1" Display="Dynamic" ErrorMessage="Photo is required" ForeColor="Red" SetFocusOnError="True" ValidationGroup="AllValidators">*</asp:RequiredFieldValidator>

                        <asp:Label ID="Label2" runat="server" ForeColor="Red"></asp:Label>

                    </td>
                </tr>

                <tr>
                    <td class="auto-style2">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label5" runat="server" Text="What is your role?"></asp:Label>
                    </td>
                    <td class="auto-style3">
                        

                        <asp:RadioButton ID="rbtn_student" runat="server"  CausesValidation="True" EnableTheming="True" GroupName="RoleGroup" Text="Student" />
                        <asp:RadioButton ID="rbtn_staff" runat="server" GroupName="RoleGroup" Text="Staff" />
                    </td>
                    <td>

                        &nbsp;</td>
                </tr>

            </table>
            <br />
            <asp:Button ID="btn_submit" runat="server" OnClick="btn_submit_Click" Text="Submit" Height="39px" Width="92px" />
            <br />
            <asp:Label ID="Label3" runat="server" ForeColor="Red"></asp:Label>
        </div>
    </form>
</body>
</html>
