using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

namespace tryapp
{
    public partial class Maps : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ArrayList det = new ArrayList();
            det = (ArrayList)Session["details"];

            TextBox1.Text = "0";
            TextBox2.Text = "0";
            TextBox3.Text = (String)Session["entity"];

            if(Session["accsize"]!= null || Session["consize"]!= null)
            {
            TextBox1.Text = Convert.ToString(Session["accsize"]);
            TextBox2.Text = Convert.ToString(Session["consize"]);
                }
            
                DropDownList1.DataSource = (ArrayList)det[1];
                DropDownList1.DataBind();
                        
                DropDownList2.DataSource = (ArrayList)det[2];
                DropDownList2.DataBind();

                DropDownList3.DataSource = (ArrayList)det[0];
                DropDownList3.DataBind();
                
                DropDownList4.DataSource = (ArrayList)det[3];
                DropDownList4.DataBind();

                DropDownList5.DataSource = (ArrayList)det[4];
                DropDownList5.DataBind();

                DropDownList6.DataSource = (ArrayList)det[5];
                DropDownList6.DataBind();

                DropDownList7.DataSource = (ArrayList)det[6];
                DropDownList7.DataBind();
           
               

        }

       
        
          
    }
}