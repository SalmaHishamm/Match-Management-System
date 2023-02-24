<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrR.aspx.cs" Inherits="Milestone3.CrR" %>

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
            <asp:TextBox ID="Ncr" runat="server"></asp:TextBox>
        </p>
   
        Club Name:<p>
            <asp:TextBox ID="CNcr" runat="server"></asp:TextBox>
        </p>
        Username:<p>
            <asp:TextBox ID="Ucr" runat="server"></asp:TextBox>
        </p>
  
        Password:<p>
            <asp:TextBox ID="Pcr" runat="server"></asp:TextBox>
        </p>
            <asp:Button ID="Rcr" runat="server" Text="Register" OnClick="Rcr_Click" />

    &nbsp;
            <asp:Button ID="Button2" runat="server" Text="Sign In" OnClick="Rcr_Click2" />

    </form>

</body>
</html>
