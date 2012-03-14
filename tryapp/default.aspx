<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="tryapp._default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CRM</title>
    <style type="text/css">
        .style1
        {
            text-align: center;
        }
        .style2
        {
            font-family: Arial;
        }
    </style>
</head>
<body>

<div align="center">
<h1 style="height: 37px">Welcome to CRM Access</h1>
    
    </div>
    <form id="form1" runat="server">
    <div align="center"  style="height: 359px;">
        <br />
        <asp:Label ID="Label1" runat="server" Text="Label" CssClass="style2">Live ID</asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextBox1" runat="server" Width="203px"></asp:TextBox>
         <br />
        <br />
         <asp:Label ID="Label2" runat="server" Text="Label" CssClass="style2">Password</asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextBox2" runat="server" Width="204px" TextMode="Password"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="Login" onclick="Button1_Click" />

    
        <br />
        <br />
        <asp:Label ID="Label3" runat="server"></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label4" runat="server"></asp:Label>

    
        <br />
        <br />
        

    
    </div>
    <div class="style1">
        <br />
        <br />
    <asp:Label ID="Label5" runat="server"></asp:Label>
        <br />
        <br />
        <asp:DropDownList ID="DropDownList1" runat="server" Visible="False" >
        </asp:DropDownList>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="Go" 
            Visible="False" />

    
        <br />
        </div>
    </form>
</body>
</html>
