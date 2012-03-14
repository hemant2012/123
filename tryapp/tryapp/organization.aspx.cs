using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace tryapp
{
    public partial class organization : System.Web.UI.Page
    {
        public void Page_Load(object sender, EventArgs e)
        {
           // int input;
            
            
          /* Label1.Text = Convert.ToString(GlobalClass.Sel_ind);
            Label2.Text = GlobalClass.U_n ;
            Label3.Text = GlobalClass.P_w ;*/

           try
            {
               Microsoft.Crm.Sdk.Samples.ServerConnection run = new Microsoft.Crm.Sdk.Samples.ServerConnection();
               Microsoft.Crm.Sdk.Samples.ServerConnection.Configuration con= run.Gettest(GlobalClass.U_n, GlobalClass.P_w, GlobalClass.Sel_ind);
                orgname.Text = con.OrganizationName;

               
            }
            catch (System.TimeoutException ex)
            {
                Label1.Text = "The application  terminated with an error.";
                Label2.Text = ex.Message;
            }
            catch (System.Exception ex)
            {
                Label1.Text = "The application  terminated with an error.";
                Label2.Text = ex.Message;

            }

            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

       

        protected void Button2_Click(object sender, EventArgs e)
        {

        }
        protected void Button3_Click(object sender, EventArgs e)
        {

        }
    }
}