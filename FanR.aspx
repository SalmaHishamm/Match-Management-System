<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FanR.aspx.cs" Inherits="Milestone3.FanR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            National ID:<br />
            <asp:TextBox ID="NidF" runat="server" ></asp:TextBox>
        </div>
        Name:<p>
            <asp:TextBox ID="NF" runat="server"></asp:TextBox>
        </p>
        <p>
            Username:</p>
        <p>
            <asp:TextBox ID="UNF" runat="server"></asp:TextBox>
        </p>
        <p>
            Password:</p>
        <div>
            <asp:TextBox ID="PF" runat="server"></asp:TextBox>
        </div>
        Birthdate:<p>
            <asp:TextBox ID="BDF" runat="server" placeholder="YY/MM/DD"></asp:TextBox>
        </p>
        <p>
            Address:</p>
        <p>
            <asp:TextBox ID="Address" runat="server"></asp:TextBox>
        </p>
        <p>
            Phone Number:</p>
        <p>
            <asp:TextBox ID="PhoneNumber" runat="server"></asp:TextBox>
        </p>
        <p>
            &nbsp;</p>
        <p>
            <asp:Button ID="register" runat="server" Text="Register" OnClick="register_Click" />
        &nbsp;
            <asp:Button ID="Button1" runat="server" Text="Sign In" Width="84px" OnClick="Button1_Click" />
        </p>
        <p>
            &nbsp;</p>
    </form>
</body>
</html>
