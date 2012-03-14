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
        static String m_un = "";
        static String m_ps = "";
        static int m_ind;

        public static String U_n
        {
            get { return m_un; }
            set { m_un = value; }
        }
        public static String P_w
        {
            get { return m_ps; }
            set { m_ps = value; }
        }
        public static int Sel_ind
        {
            get { return m_ind; }
            set { m_ind = value; }
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
                 String uname = TextBox1.Text;
                 String pword = TextBox2.Text;
                 GlobalClass.U_n = uname;
                 GlobalClass.P_w = pword;
 
                String[] c=new String[50];

                Microsoft.Crm.Sdk.Samples.ServerConnection run = new Microsoft.Crm.Sdk.Samples.ServerConnection();
                 Array.Copy(run.GetOrganizations(uname, pword), c, 50);
                Label3.Text = "Login Successful";

                int i = 0;
                while (i < c.Length & c[i] != null)
                {
                  
                    DropDownList1.Items.Add(c[i]);
                    i++;
                }

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
        }

        public void Button2_Click(object sender, EventArgs e)
        {

           int sel = DropDownList1.SelectedIndex;
           GlobalClass.Sel_ind = sel;
           Response.Redirect("organization.aspx");
      }
        
        
    }
}