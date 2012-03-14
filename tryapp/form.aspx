<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="form.aspx.cs" Inherits="tryapp.form" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function drop() {
            var d1 = document.getElementById("DropDownList1");
            var id = document.getElementById("Label2").value;
            var name = document.getElementById("TextBox1").value;
            var email = document.getElementById("TextBox2").value;
            var city = document.getElementById("TextBox3").value;
            var country = document.getElementById("TextBox4").value;
            var lat = document.getElementById("TextBox5").value;
            var lon = document.getElementById("TextBox6").value;


            d1.options[0].value = id;
            d1.options[1].value = name;
            d1.options[2].value = email;
            d1.options[3].value = city;
            d1.options[4].value = country;
            d1.options[5].value = lat;
            d1.options[6].value = lon;



        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <br />
        <asp:Label ID="Label1" runat="server"></asp:Label>
        <br />
        <asp:Label ID="Label2" runat="server"></asp:Label>
        <br />
        <br />
    
        Name<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <br />
        <br />
        Email<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <br />
        <br />
        City<asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        <br />
        <br />
        Country<asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
        <br />
        <br />
        latitude<asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
        <br />
        <br />
        longitude<asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
    
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" OnClientClick="drop()" Text="Save" />
        <br />
        <br />
        <asp:Label ID="Label3" runat="server"></asp:Label>
        <br />
    
    </div>
    </form>
</body>
</html>
