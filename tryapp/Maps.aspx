<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Maps.aspx.cs" Inherits="tryapp.Maps" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>map</title>
    <script type="text/javascript" src="http://maps.google.com/maps?file=api&amp%3Bv=2&amp%3Bkey=ABQIAAAANGcb2Qiwo4p6YOFxZVbRNxTgtNaB4CGaav_MdC6h2IdB9kQo7xS6IQ0PIubRKfcpojT0qLAFkIbgVw&sensor=false">
    </script>
    <script type="text/javascript">

        function mapall() {

            var d1 = document.getElementById("DropDownList1");
            var d2 = document.getElementById("DropDownList2");
            var name = document.getElementById("DropDownList3");
            var info1 = document.getElementById("DropDownList4");
            var info2 = document.getElementById("DropDownList5");
            var info3 = document.getElementById("DropDownList6");
            var id = document.getElementById("DropDownList7");
            var size1 = parseInt(document.getElementById("TextBox1").value);
            var size2 = parseInt(document.getElementById("TextBox2").value);
           
           

            var x = new Array(d1.options.length);
            var y = new Array(d2.options.length);
            var nm = new Array(name.options.length);
            var i1 = new Array(info1.options.length);
            var i2 = new Array(info2.options.length);
            var i3 = new Array(info3.options.length);
            var i;
            for (i = 0; i < nm.length; i++) {
                nm[i] = name.options[i].value;
                i1[i] = info1.options[i].value;
                i2[i] = info2.options[i].value;
                i3[i] = info3.options[i].value;
            }


            for (i = 0; i < x.length && i < y.length; i++) {
                x[i] = d1.options[i].value;
                y[i] = d2.options[i].value;

            }

            if (GBrowserIsCompatible()) {
                var map = new GMap2(document.getElementById("map_canvas"));
                map.addControl(new GMapTypeControl());
                map.setCenter(new GLatLng(x[0], y[0]), 8);
                map.setUIToDefault();
            }

            function createMarkers(point, number) {
                var dis_nm = nm[i];
                var dis_i1 = i1[i];
                var dis_i2 = i2[i];
                var dis_i3 = i3[i];
                var lt  = d1.options[i].value;
                var ln = d2.options[i].value;
                var _id = id.options[i].value;

                var baseIcon = new GIcon(G_DEFAULT_ICON);
                baseIcon.image = "http://www.google.com/intl/en_us/mapfiles/ms/micons/red-dot.png";
                baseIcon.shadow = "http://www.google.com/mapfiles/shadow50.png";
                baseIcon.iconSize = new GSize(20, 34);
                baseIcon.shadowSize = new GSize(37, 34);
                baseIcon.iconAnchor = new GPoint(9, 34);
                baseIcon.infoWindowAnchor = new GPoint(9, 2);
                markerOptions = { icon: baseIcon };


                var marker = new GMarker(point, markerOptions);
              
                marker.value = number;
                GEvent.addListener(marker, "click", function () {
                    var dis_en = document.getElementById("TextBox3").value
                    var url = "form.aspx?";
                    url += "entity=" +dis_en  + "&";
                    url += "name=" + dis_nm + "&";
                    url += "email=" + dis_i1 + "&";
                    url += "city=" + dis_i2 + "&";
                    url += "country=" + dis_i3 + "&";
                    url += "lat=" + lt + "&";
                    url += "lon=" + ln + "&";
                    url += "id=" + _id ;
                    var myHtml = "<table>" +
                         "<tr><td></td> <td>"+dis_nm+" </td> </tr>" +
                         "<tr><td></td> <td>" + dis_i1 + "</td> </tr>" +
                          "<tr><td></td> <td>" + dis_i2 + "</td> </tr>" +
                           "<tr><td></td> <td>" + dis_i3 + "</td> </tr>" +
                         "<tr><td><a href='"+url+"' target='_new'>Edit..</a></td><td></td></tr>";
                    
                    map.openInfoWindowHtml(point, myHtml);
                  
                    name.options[marker.value].selected = true;
                    info1.options[marker.value].selected = true;
                    info2.options[marker.value].selected = true;
                    info3.options[marker.value].selected = true;
                    d1.options[marker.value].selected = true;
                    d2.options[marker.value].selected = true;
                    id.options[marker.value].selected = true;
                    document.getElementById("TextBox4").value = marker.value;
                                   
                    
                });

               

                return marker;


            }


            function createMarker(point, number) {
                var dis_nm = nm[i];
                var dis_i1 = i1[i];
                var dis_i2 = i2[i];
                var dis_i3 = i3[i];
                var lt = d1.options[i].value;
                var ln = d2.options[i].value;
                var _id = id.options[i].value;
               
              


                if (i < size1) {
                    var redIcon = new GIcon(G_DEFAULT_ICON);
                    redIcon.image = "http://www.google.com/intl/en_us/mapfiles/ms/micons/red-dot.png";
                    redIcon.shadow = "http://www.google.com/mapfiles/shadow50.png";
                    redIcon.iconSize = new GSize(20, 34);
                    redIcon.shadowSize = new GSize(37, 34);
                    redIcon.iconAnchor = new GPoint(9, 34);
                    redIcon.infoWindowAnchor = new GPoint(9, 2);

                    markerOptions = { icon: redIcon };
                    var entity_type = "Account";
                }
                else if (i >= size1 && i < size2) {
                    var blueIcon = new GIcon(G_DEFAULT_ICON);
                    blueIcon.image = "http://www.google.com/intl/en_us/mapfiles/ms/micons/blue-dot.png";
                    blueIcon.shadow = "http://www.google.com/mapfiles/shadow50.png";
                    blueIcon.iconSize = new GSize(20, 34);
                    blueIcon.shadowSize = new GSize(37, 34);
                    blueIcon.iconAnchor = new GPoint(9, 34);
                    blueIcon.infoWindowAnchor = new GPoint(9, 2);

                    markerOptions = { icon: blueIcon };
                    var entity_type = "Contact";
                }
                else if (i >= size2) {
                    var yellowIcon = new GIcon(G_DEFAULT_ICON);
                    yellowIcon.image = "http://www.google.com/intl/en_us/mapfiles/ms/micons/yellow-dot.png";
                    yellowIcon.shadow = "http://www.google.com/mapfiles/shadow50.png";
                    yellowIcon.iconSize = new GSize(20, 34);
                    yellowIcon.shadowSize = new GSize(37, 34);
                    yellowIcon.iconAnchor = new GPoint(9, 34);
                    yellowIcon.infoWindowAnchor = new GPoint(9, 2);
                
                markerOptions = { icon: yellowIcon };
                    var entity_type = "Lead";
                }
               

              
                var marker = new GMarker(point, markerOptions);

                marker.value = number;
                GEvent.addListener(marker, "click", function () {
                   
                    var url = "form.aspx?";
                    url += "entity=" + entity_type + "&";
                    url += "name=" + dis_nm + "&";
                    url += "email=" + dis_i1 + "&";
                    url += "city=" + dis_i2 + "&";
                    url += "country=" + dis_i3 + "&";
                    url += "lat=" + lt + "&";
                    url += "lon=" + ln + "&";
                    url += "id=" + _id;
                    var myHtml = "<table>" +
                        "<tr><td></td> <td><b>" + entity_type + " </b></td> </tr>" +
                         "<tr><td></td> <td>" + dis_nm + " </td> </tr>" +
                         "<tr><td></td> <td>" + dis_i1 + "</td> </tr>" +
                          "<tr><td></td> <td>" + dis_i2 + "</td> </tr>" +
                           "<tr><td></td> <td>" + dis_i3 + "</td> </tr>" +
                         "<tr><td><a href='" + url + "' target='_new'>Edit..</a></td><td></td></tr>";

                    map.openInfoWindowHtml(point, myHtml);

                    name.options[marker.value].selected = true;
                    info1.options[marker.value].selected = true;
                    info2.options[marker.value].selected = true;
                    info3.options[marker.value].selected = true;
                    d1.options[marker.value].selected = true;
                    d2.options[marker.value].selected = true;
                    id.options[marker.value].selected = true;
                    document.getElementById("TextBox4").value = marker.value;
                                   
                });
                return marker;
            }


            for (i = 0; i < x.length && i < y.length; i++) {
                var point = new GLatLng(x[i], y[i]);
                if (size1 == 0 && size2 == 0) {
                    map.addOverlay(createMarkers(point, i));
                }
                else {
                    map.addOverlay(createMarker(point, i));
                }
            }


        }

        function saveData() {

            marker.closeInfoWindow();
            
        }

     

    
    </script>
</head>
<body onload="mapall()" >
    <form id="form1" runat="server">
    
    <div id="map_canvas" style="height: 500px; width: 500px;">
    </div>
    &nbsp;
    <asp:DropDownList ID="DropDownList1" runat="server">
    </asp:DropDownList>
    <asp:DropDownList ID="DropDownList2" runat="server">
    </asp:DropDownList>
    <asp:DropDownList ID="DropDownList3" runat="server" >
    </asp:DropDownList>
    <asp:DropDownList ID="DropDownList4" runat="server">
    </asp:DropDownList>
    <asp:DropDownList ID="DropDownList5" runat="server">
    </asp:DropDownList>
    <asp:DropDownList ID="DropDownList6" runat="server"> 
    </asp:DropDownList>
    <asp:DropDownList ID="DropDownList7" runat="server">
    </asp:DropDownList>
    <br />
    
    &nbsp;<asp:TextBox ID="TextBox1" runat="server" Width="47px"></asp:TextBox>
    &nbsp;<asp:TextBox ID="TextBox2" runat="server" Width="47px"></asp:TextBox>
    &nbsp;&nbsp;<asp:TextBox ID="TextBox3" runat="server" Width="99px"></asp:TextBox>
    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="TextBox4" runat="server" Width="67px"></asp:TextBox>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </form>
</body>
</html>
