<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SmR.aspx.cs" Inherits="Milestone3.SmR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
        Name:<p>
            <asp:TextBox ID="Nsm" runat="server"></asp:TextBox>
        </p>
        Stadium Name:<p>
            <asp:TextBox ID="SN" runat="server"></asp:TextBox>
        </p>
        Username:<p>
            <asp:TextBox ID="Usm" runat="server"></asp:TextBox>
        </p>
        Password:<p>
            <asp:TextBox ID="Psm" runat="server"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="Rsm" runat="server" Text="Register" OnClick="Rsm_Click" />
        &nbsp;&nbsp;
            <asp:Button ID="Button2" runat="server" Text="Sign In" Width="84px" OnClick="Button2_Click" />
        </p>
    </form>
</body>
</html>
