using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tryapp
{
    public static class GlobalClass
    {
      
        static Microsoft.Crm.Sdk.Samples.ServerConnection.Configuration conf;
        
  

        public static Microsoft.Crm.Sdk.Samples.ServerConnection.Configuration s_conf
        {
            get { return conf; }
            set { conf = value; }
        }

    }
    public partial class _default : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        protected void Button1_Click(object sender, EventArgs e)
        {

            try
            {
                
                 Session["username"] =TextBox1.Text;
                 Session["password"]= TextBox2.Text;
 
                

                Microsoft.Crm.Sdk.Samples.ServerConnection run = new Microsoft.Crm.Sdk.Samples.ServerConnection();
                 
                Label3.Text = "Login Successful";
                Server.ScriptTimeout = 200;

                DropDownList1.DataSource = run.GetOrganizations(TextBox1.Text, TextBox2.Text);
                DropDownList1.DataBind();  

               Label5.Text = "Select Your Organization";

               DropDownList1.Visible = true;
             
               Button2.Visible = true; 
                }
            catch (System.TimeoutException ex)
            {
                Label3.Text = "The application  terminated with an error.";
                Label4.Text = ex.Message;
            }
            catch (System.Exception ex)
            {
                Label3.Text = "The application  terminated with an error.";
                Label4.Text = ex.Message;

            }
            Server.ScriptTimeout = 200;
        }

        public void Button2_Click(object sender, EventArgs e)
        {


            Session["selection"]= DropDownList1.SelectedIndex;
            Session["orgname"] = DropDownList1.SelectedItem.Value;
            
           Response.Redirect("organization.aspx");
      }
        
        
    }
}