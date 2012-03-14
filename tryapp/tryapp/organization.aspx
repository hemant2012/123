<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="organization.aspx.cs" Inherits="tryapp.organization" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .style2
        {
            font-size: x-large;
        }
        .style3
        {
            text-align: center;
        }
        .style4
        {
            font-size: xx-large;
        }
        .style5
        {
            font-size: large;
        }
    </style>
</head>
<body>

    <form id="form1" runat="server">
     <div class="style3">
         <span class="style5">Welcome to
        </span>
        <asp:Label ID="orgname" runat="server" CssClass="style5"></asp:Label></div>
    
    
    
    <div style="height: 399px; font-size: large;">
        <br />
        &nbsp;&nbsp;<span class="style2">&nbsp; You can manage your</span><br />
        <div style="width: 946px">
         <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button1" runat="server" Text="Accounts" onclick="Button1_Click" Width="73px" />
        
        <br class="style4" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server" Text="Contacts" onclick="Button2_Click" Width="73px" />
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button3" runat="server" Text="Leads" onclick="Button3_Click" Width="73px" />
        &nbsp;<br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:ListBox ID="ListBox1" runat="server" Height="124px" Visible="False" 
                Width="154px"></asp:ListBox>
        <br />
        <br /></div>
        <br />
        <br />
        <asp:Label ID="Label1" runat="server"></asp:Label>
        <br />
        <asp:Label ID="Label2" runat="server"></asp:Label>
            
        </div>
    </form>
</body>
</html>
