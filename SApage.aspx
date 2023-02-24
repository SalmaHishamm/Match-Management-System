<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SApage.aspx.cs" Inherits="Milestone3.SApage" %>

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
            Add New Club
        </p>
        <p>
            Name<asp:TextBox ID="addCname" runat="server" OnTextChanged="Ncr_TextChanged" style="margin-left: 19px" Width="163px"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Location<asp:TextBox ID="addClocation" runat="server" OnTextChanged="Ncr_TextChanged" style="margin-left: 40px" Width="165px"></asp:TextBox>
        &nbsp;&nbsp;
            <asp:Button ID="AddCdone" runat="server" OnClick="AddClub_Click" Text="Done" />
        </p>
        <p>
            &nbsp;</p>
        <p>
            Delete Club
        </p>
        <p>
            Name <asp:TextBox ID="DeleteCname" runat="server" OnTextChanged="Ncr_TextChanged" style="margin-left: 19px" Width="163px"></asp:TextBox>
        &nbsp;&nbsp;
            <asp:Button ID="DeleteCdone" runat="server" OnClick="DeleteClub_Click" Text="Done" />
        </p>
        <p>
            &nbsp;</p>
        <p>
            Add New Stadium
        </p>
        <p>
            Name
            <asp:TextBox ID="AddSname" runat="server" OnTextChanged="Ncr_TextChanged" style="margin-left: 19px" Width="163px"></asp:TextBox>
        &nbsp;&nbsp; Location
            <asp:TextBox ID="AddSlocation" runat="server" OnTextChanged="Ncr_TextChanged" style="margin-left: 19px" Width="163px"></asp:TextBox>
        &nbsp;&nbsp;&nbsp; Capacity
            <asp:TextBox ID="AddScapacity" runat="server" OnTextChanged="Ncr_TextChanged" style="margin-left: 19px" Width="163px"></asp:TextBox>
        &nbsp;&nbsp;
            <asp:Button ID="AddSdone" runat="server" OnClick="AddStadium_Click" Text="Done" />
        </p>
        <p>
            &nbsp;</p>
        <p>
            Delete Stadium
        </p>
        <p>
            Name
            <asp:TextBox ID="deleteSname" runat="server" OnTextChanged="Ncr_TextChanged" style="margin-left: 19px" Width="163px"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="DeleteSdone" runat="server" OnClick="DeleteStad_Click" Text="Done" />
        </p>
        <p>
            &nbsp;</p>
        <p>
            Block Fan
        </p>
        <p>
            National ID
            <asp:TextBox ID="BlockFnid" runat="server" OnTextChanged="Ncr_TextChanged" style="margin-left: 19px" Width="163px"></asp:TextBox>
        &nbsp;&nbsp;
            <asp:Button ID="BlockDone" runat="server" OnClick="BlockFan_Click" Text="Done" />
        </p>
        <p>
            &nbsp;</p>
 &nbsp;</p>
        <p>
            <asp:Button ID="LogOut1" runat="server" OnClick="LogOutSA_Click" Text="Log Out" />
        </p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
    </form>
</body>
</html>
