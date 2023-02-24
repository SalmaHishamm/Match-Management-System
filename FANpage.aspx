<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FANpage.aspx.cs" Inherits="Milestone3.FANpage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="height: 326px">
    <form id="form1" runat="server">
        <div>
            Start Time<asp:TextBox ID="datestart" placeholder="YY/MM/DD hh:mm:ss" runat="server" style="margin-left: 41px" Width="166px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button1" runat="server" Text="Show Available Matches" Width="192px" OnClick="Button1_Click" />
        </div>
        <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Height="240px" style="margin-top: 42px" Width="368px" OnSelectedIndexChanged="GridView1_SelectedIndexChanged1">
            <AlternatingRowStyle BackColor="White" />
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
        <br />
        <asp:GridView ID="Grid2" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Height="240px" OnSelectedIndexChanged="GridView2_SelectedIndexChanged" style="margin-top: 42px" Width="368px">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:ButtonField CommandName="Select" HeaderText="Matches" ShowHeader="True" Text="Purchase" />
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
        <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
        <br />
        <br />
            <asp:Button ID="Button2" runat="server" Text="Log Out" Width="165px" OnClick="Button2_Click" />
    </form>
</body>
</html>
