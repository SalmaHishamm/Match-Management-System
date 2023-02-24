<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SamR.aspx.cs" Inherits="Milestone3.SamR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Name:</div>
        <p>
            <asp:TextBox ID="Nsam" runat="server"></asp:TextBox>
        </p>
        <div>
            Username:</div>
        <p>
            <asp:TextBox ID="Usam" runat="server"></asp:TextBox>
        </p>
        <div>
            Password:</div>
        <p>
            <asp:TextBox ID="Psam" runat="server"></asp:TextBox>
        </p>
        <p>
            &nbsp;</p>
        <asp:Button ID="Rsam" runat="server" Text="Register" OnClick="Rsam_Click" />
    &nbsp;&nbsp;
        <asp:Button ID="signinsam" runat="server" OnClick="Button1_Click" Text="Sign In" Width="84px" />
    </form>
</body>
</html>
