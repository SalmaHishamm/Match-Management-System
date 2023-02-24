<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SAMpage.aspx.cs" Inherits="Milestone3.SAMpage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Add New Match<br />
            <br />
            Host Club Name:<asp:TextBox ID="HCN" runat="server" OnTextChanged="TextBox1_TextChanged" style="margin-bottom: 0px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Guest Club Name:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="GCN" runat="server" OnTextChanged="TextBox1_TextChanged" style="margin-bottom: 0px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Start Time:<asp:TextBox ID="MST" runat="server" placeholder="YY/MM/DD hh:mm:ss"  OnTextChanged="TextBox1_TextChanged" style="margin-bottom: 0px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; End Time:<asp:TextBox ID="MET" runat="server"  placeholder="YY/MM/DD hh:mm:ss" OnTextChanged="TextBox1_TextChanged" style="margin-bottom: 0px"></asp:TextBox>
            <asp:Button ID="AMd" runat="server" Text="Done" OnClick="AMd_Click" style="height: 29px" />
            <br />
            <br />
            <br />
            Delete Match<br />
            <br />
            Host Club Name:<asp:TextBox ID="HCN0" runat="server" OnTextChanged="TextBox1_TextChanged" style="margin-bottom: 0px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Guest Club Name:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="GCN0" runat="server" OnTextChanged="TextBox1_TextChanged" style="margin-bottom: 0px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Start Time:<asp:TextBox ID="MST0" runat="server"  placeholder="YY/MM/DD hh:mm:ss" OnTextChanged="TextBox1_TextChanged" style="margin-bottom: 0px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; End Time:<asp:TextBox ID="MET0" runat="server"  placeholder="YY/MM/DD hh:mm:ss" OnTextChanged="TextBox1_TextChanged"  style="margin-bottom: 0px"></asp:TextBox>
            <asp:Button ID="DMd" runat="server" Text="Done" OnClick="DMd_Click" />
            <br />
            <br />
            <br />
            <asp:Button ID="VAPM" runat="server" Text="Already Played Matches" Width="298px" OnClick="VAPM_Click" />
            &nbsp;&nbsp;
            <br />
            <br />
            <br />
            <asp:Button ID="VAM" runat="server" Text=" All Upcoming Matches" Width="295px" OnClick="VAM_Click" />
            <br />
            <br />
            <br />
            <asp:Button ID="VNPM" runat="server" Text="Pairs Never Played" Width="296px" OnClick="VNPM_Click" />
            <br />
            <br />
            <br />
            <br />
        </div>
        <p>
            <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField />
                </Columns>
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
        </p>
        <p>
            &nbsp;</p>
        <p>
            <asp:Button ID="Logout" runat="server" Text="Log Out" Width="164px" OnClick="LO_Click" />
        </p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
    </form>
</body>
</html>
