<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Milestone3.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
        <p>
            Please Log in
        </p>
        <p>
            Username:</p>
        <asp:TextBox ID="username" runat="server"></asp:TextBox>
        <br />
        Password:<br />
        <asp:TextBox ID="password" runat="server"></asp:TextBox>
        <p>
            <asp:Button ID="signin" runat="server" OnClick="login" Text="Log In" />
        </p>
        <p>
            Doesn&#39;t have an account ? Register here as </p>
        <p>
            <asp:Button ID="Fan" runat="server" Text="Fan" Width="205px" OnClick="Fan_Click" />

        </p>
        <p>

        <asp:Button ID="SportsAssociationManager" runat="server" Text="SportsAssociationManager" Width="207px" OnClick="SportsAssociationManager_Click" />
            </p>
        <p>
            <asp:Button ID="StadiumManager" runat="server" Text="StadiumManager" Width="207px" OnClick="StadiumManager_Click" />
            </p>
        <p>
            <asp:Button ID="ClubRepresentative" runat="server" Text="ClubRepresentative" Width="205px" OnClick="ClubRepresentative_Click" />
        </p>
    </form>
</body>
</html>
