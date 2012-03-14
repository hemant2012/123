using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

namespace tryapp
{
    public partial class form : System.Web.UI.Page
    {
        String name;
        String email;
        String city;
        String country;
        String lat;
        String lon;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Text = Request.QueryString["entity"];
            Label2.Text = Request.QueryString["id"];
            
           /* name = Request.QueryString["name"];
            email = Request.QueryString["email"];
            city = Request.QueryString["city"];
            country = Request.QueryString["country"];
            lat = Request.QueryString["lat"];
            lon = Request.QueryString["lon"];
        

            TextBox1.Text = name;
            TextBox2.Text = email;
            TextBox3.Text = city;
            TextBox4.Text = country;
            TextBox5.Text = lat;
            TextBox6.Text = lon;*/
        
        }
           
       


        

        protected void Button1_Click(object sender, EventArgs e)
        {
             
             
            String id = Label2.Text;
              
               name = TextBox1.Text;
               email = TextBox2.Text;
               city = TextBox3.Text;
               country = TextBox4.Text;
               lat = TextBox5.Text;
               lon = TextBox6.Text;

           
            ArrayList form_data = new ArrayList();
            form_data.Add(id);
            form_data.Add(name);
            form_data.Add(email);
            form_data.Add(city);
            form_data.Add(country);
            form_data.Add(lat);
            form_data.Add(lon);


          
          
          tryapp.Operations action = new tryapp.Operations();
            
           if (Label1.Text == "Accounts")
            {
                action.accUpdate(GlobalClass.s_conf, form_data);
                Label3.Text = "Account updated";
            }
            if (Label1.Text == "Contacts")
            {
                action.conUpdate(GlobalClass.s_conf, form_data);
                Label3.Text = "Contact updated";

            }
            if (Label1.Text == "Leads")
            {

            }

        }
    }
}