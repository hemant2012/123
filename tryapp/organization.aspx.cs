using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ServiceModel;
using System.ServiceModel.Description;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Crm.Sdk.Messages;
using System.Collections;




namespace tryapp
{
    
    
  
    public partial class organization : System.Web.UI.Page
    {
        ArrayList criteria = new ArrayList();
        
        ArrayList lead_criteria = new ArrayList();
        

        public void Page_Load(object sender, EventArgs e)
        {
           String user=(String)Session["username"];
            String pass=(String)Session["password"];
            int a=(int)Session["selection"];
               Microsoft.Crm.Sdk.Samples.ServerConnection run = new Microsoft.Crm.Sdk.Samples.ServerConnection();

               GlobalClass.s_conf = run.Gettest(user,pass,a);
               orgname.Text =(String)Session["orgname"];
               
       }

        
        protected void Button1_Click(object sender, EventArgs e)
        {
            
            tryapp.Operations action = new tryapp.Operations();
            Session["details"] = action.criteria(GlobalClass.s_conf, DropDownList1.SelectedIndex, DropDownList2.SelectedIndex);
            Session["entity"] = DropDownList1.SelectedItem.Value;
            Session["accsize"] = "0";
            Session["consize"] = "0";

           Response.Redirect("maps.aspx");

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            tryapp.Operations action = new tryapp.Operations();
            Session["details"] = action.GetAll(GlobalClass.s_conf);
            Session["accsize"] = action.acc_size;
            Session["consize"] = action.c_size;
          
           
            Response.Redirect("maps.aspx");

        }

       

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void Button3_Click(object sender, EventArgs e)
        {

        }

           
    }

    public class Operations
    {
        private OrganizationServiceProxy _serviceProxy;
        private IOrganizationService _service;
        public int acc_size = 0;
        public int c_size = 0;

        ArrayList info1 = new ArrayList();
        ArrayList info2 = new ArrayList();
        ArrayList info3 = new ArrayList();
        ArrayList lat = new ArrayList();
        ArrayList lon = new ArrayList();
        ArrayList id = new ArrayList();
        ArrayList en_names = new ArrayList();
        ArrayList en_det = new ArrayList();




        public ArrayList GetAll(Microsoft.Crm.Sdk.Samples.ServerConnection.Configuration serverconfig)
        {
            try
            {
                using (_serviceProxy = Microsoft.Crm.Sdk.Samples.ServerConnection.GetOrganizationProxy(serverconfig))
                {
                    _serviceProxy.EnableProxyTypes();
                    _service = (IOrganizationService)_serviceProxy;
                    ServiceContext svcContext = new ServiceContext(_service);

                   

                    var accounts = from a in svcContext.AccountSet
                                   select new Account
                                   {
                                       Name = a.Name,
                                       EMailAddress1 = a.EMailAddress1,
                                       Address1_City = a.Address1_City,
                                       Address1_Country = a.Address1_Country,
                                       Address1_Latitude = a.Address1_Latitude,
                                       Address1_Longitude = a.Address1_Longitude,
                                       AccountId=a.AccountId
                                   };

                    foreach (var a in accounts)
                    {
                        en_names.Add(a.Name);
                        lat.Add(a.Address1_Latitude);
                        lon.Add(a.Address1_Longitude);
                        info1.Add(a.EMailAddress1);
                        info2.Add(a.Address1_City);
                        info3.Add(a.Address1_Country);
                        id.Add(a.AccountId);
                    }

                    acc_size = en_names.Count;


                    var queryContacts = from c in svcContext.ContactSet
                                        select new Contact
                                        {
                                            FirstName = c.FirstName,
                                            Address1_Latitude = c.Address1_Latitude,
                                            Address1_Longitude = c.Address1_Longitude,
                                            EMailAddress1 = c.EMailAddress1,
                                            Address1_City = c.Address1_City,
                                            Address1_Country = c.Address1_Country,
                                            ContactId=c.ContactId
                                        };


                    foreach (var c in queryContacts)
                    {
                        en_names.Add(c.FirstName);
                        lat.Add(c.Address1_Latitude);
                        lon.Add(c.Address1_Longitude);
                        info1.Add(c.EMailAddress1);
                        info2.Add(c.Address1_City);
                        info3.Add(c.Address1_Country);
                        id.Add(c.ContactId);

                    }
                    c_size = en_names.Count;


                    var leads = from l in svcContext.LeadSet
                                select new Lead
                                {
                                    FirstName = l.FirstName,
                                    Address1_Latitude = l.Address1_Latitude,
                                    Address1_Longitude = l.Address1_Longitude,
                                    EMailAddress1 = l.EMailAddress1,
                                    Address1_City = l.Address1_City,
                                    Address1_Country = l.Address1_Country,
                                    LeadId=l.LeadId
                                };

                    foreach (var l in leads)
                    {
                        en_names.Add(l.FirstName);
                        lat.Add(l.Address1_Latitude);
                        lon.Add(l.Address1_Longitude);
                        info1.Add(l.EMailAddress1);
                        info2.Add(l.Address1_City);
                        info3.Add(l.Address1_Country);
                        id.Add(l.LeadId);
                    }

                    en_det.Add(en_names);
                    en_det.Add(lat);
                    en_det.Add(lon);
                    en_det.Add(info1);
                    en_det.Add(info2);
                    en_det.Add(info3);
                    en_det.Add(id);


                    return en_det;
                }
            }
            catch (FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault>)
            {
                throw;
            }
        }

        public ArrayList GetAccounts(Microsoft.Crm.Sdk.Samples.ServerConnection.Configuration serverconfig)
        {
            try
            {
                using (_serviceProxy = Microsoft.Crm.Sdk.Samples.ServerConnection.GetOrganizationProxy(serverconfig))
                {
                    _serviceProxy.EnableProxyTypes();
                    _service = (IOrganizationService)_serviceProxy;
                    ServiceContext svcContext = new ServiceContext(_service);


                   
                    var accounts = from a in svcContext.AccountSet
                                   select new Account
                                   {
                                       Name = a.Name,
                                       EMailAddress1 = a.EMailAddress1,
                                       Address1_City = a.Address1_City,
                                       Address1_Country = a.Address1_Country,
                                       Address1_Latitude = a.Address1_Latitude,
                                       Address1_Longitude = a.Address1_Longitude,
                                       AccountId=a.AccountId
                                   };

                    foreach (var a in accounts)
                    {
                        en_names.Add(a.Name);
                        lat.Add(a.Address1_Latitude);
                        lon.Add(a.Address1_Longitude);
                        info1.Add(a.EMailAddress1);
                        info2.Add(a.Address1_City);
                        info3.Add(a.Address1_Country);
                        id.Add(a.AccountId);
                    }
                    en_det.Add(en_names);
                    en_det.Add(lat);
                    en_det.Add(lon);
                    en_det.Add(info1);
                   en_det.Add(info2);
                    en_det.Add(info3);
                    en_det.Add(id);
                    return en_det;
                }
            }
            catch (FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault>)
            {
                throw;
            }
        }
        public ArrayList GetContacts(Microsoft.Crm.Sdk.Samples.ServerConnection.Configuration serverconfig)
        {
            try
            {
                using (_serviceProxy = Microsoft.Crm.Sdk.Samples.ServerConnection.GetOrganizationProxy(serverconfig))
                {
                    _serviceProxy.EnableProxyTypes();
                    _service = (IOrganizationService)_serviceProxy;
                    ServiceContext svcContext = new ServiceContext(_service);

                  
                    var queryContacts = from c in svcContext.ContactSet
                                        
                                        select new Contact
                                        {
                                            FirstName = c.FirstName,
                                            Address1_Latitude = c.Address1_Latitude,
                                            Address1_Longitude = c.Address1_Longitude,
                                            EMailAddress1 = c.EMailAddress1,
                                            Address1_City = c.Address1_City,
                                            Address1_Country = c.Address1_Country,
                                            ContactId=c.ContactId


                                        };


                    foreach (var c in queryContacts)
                    {
                        en_names.Add(c.FirstName);
                        lat.Add(c.Address1_Latitude);
                        lon.Add(c.Address1_Longitude);
                        info1.Add(c.EMailAddress1);
                        info2.Add(c.Address1_City);
                        info3.Add(c.Address1_Country);
                        id.Add(c.ContactId);

                    }

                   en_det.Add(en_names);
                    en_det.Add(lat);
                    en_det.Add(lon);
                    en_det.Add(info1);
                    en_det.Add(info2);
                    en_det.Add(info3);
                    en_det.Add(id);
                    return en_det;

                }
            }
            catch (FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault>)
            {
                throw;
            }
        }

        public ArrayList GetLeads(Microsoft.Crm.Sdk.Samples.ServerConnection.Configuration serverconfig)
        {
            try
            {
                using (_serviceProxy = Microsoft.Crm.Sdk.Samples.ServerConnection.GetOrganizationProxy(serverconfig))
                {
                    _serviceProxy.EnableProxyTypes();
                    _service = (IOrganizationService)_serviceProxy;
                    ServiceContext svcContext = new ServiceContext(_service);




                    var leads = from l in svcContext.LeadSet
                                select new Lead
                                {
                                    FirstName = l.FirstName,
                                    EMailAddress1 = l.EMailAddress1,
                                    Address1_City = l.Address1_City,
                                    Address1_Country = l.Address1_Country,
                                    Address1_Latitude = l.Address1_Latitude,
                                    Address1_Longitude = l.Address1_Longitude,
                                    LeadId=l.LeadId
                                };

                    foreach (var l in leads)
                    {
                        en_names.Add(l.FirstName);
                        lat.Add(l.Address1_Latitude);
                        lon.Add(l.Address1_Longitude);
                        info1.Add(l.EMailAddress1);
                        info2.Add(l.Address1_City);
                        info3.Add(l.Address1_Country);
                        id.Add(l.LeadId);
                    }


                    en_det.Add(en_names);
                    en_det.Add(lat);
                    en_det.Add(lon);
                    en_det.Add(info1);
                    en_det.Add(info2);
                    en_det.Add(info3);
                    en_det.Add(id);


                    return en_det;
                }


            }
            catch (FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault>)
            {
                throw;
            }

        }

      

        public ArrayList criteria(Microsoft.Crm.Sdk.Samples.ServerConnection.Configuration serverconfig, int set, int subset)
        {
            try
            {
                using (_serviceProxy = Microsoft.Crm.Sdk.Samples.ServerConnection.GetOrganizationProxy(serverconfig))
                {
                    _serviceProxy.EnableProxyTypes();
                    _service = (IOrganizationService)_serviceProxy;
                    ServiceContext svcContext = new ServiceContext(_service);
                    if (set == 0)
                    {
                        if (subset == 0)
                        {
                            en_det = GetAccounts(serverconfig) ;
                        }
                        if (subset == 1)
                        {
                            var accounts = from a in svcContext.AccountSet
                                           where a.CreditLimit.Value > 10000
                                           select new Account
                                           {
                                               Name = a.Name,
                                               EMailAddress1 = a.EMailAddress1,
                                               Address1_City = a.Address1_City,
                                               Address1_Country = a.Address1_Country,
                                               Address1_Latitude = a.Address1_Latitude,
                                               Address1_Longitude = a.Address1_Longitude,
                                                AccountId=a.AccountId
                                           };

                            foreach (var a in accounts)
                            {
                                en_names.Add(a.Name);
                                lat.Add(a.Address1_Latitude);
                                lon.Add(a.Address1_Longitude);
                                info1.Add(a.EMailAddress1);
                                info2.Add(a.Address1_City);
                                info3.Add(a.Address1_Country);
                                id.Add(a.AccountId);
                            }

                            en_det.Add(en_names);
                            en_det.Add(lat);
                            en_det.Add(lon);
                            en_det.Add(info1);
                            en_det.Add(info2);
                            en_det.Add(info3);
                            en_det.Add(id);

                        }
                        if (subset == 2)
                        {
                            var accounts = from a in svcContext.AccountSet
                                           where a.CreditLimit.Value < 10000
                                           select new Account
                                           {
                                               Name = a.Name,
                                               EMailAddress1 = a.EMailAddress1,
                                               Address1_City = a.Address1_City,
                                               Address1_Country = a.Address1_Country,
                                               Address1_Latitude = a.Address1_Latitude,
                                               Address1_Longitude = a.Address1_Longitude,
                                                AccountId=a.AccountId
                                           };

                            foreach (var a in accounts)
                            {
                                en_names.Add(a.Name);
                                lat.Add(a.Address1_Latitude);
                                lon.Add(a.Address1_Longitude);
                                info1.Add(a.EMailAddress1);
                                info2.Add(a.Address1_City);
                                info3.Add(a.Address1_Country);
                                id.Add(a.AccountId);
                            }

                            en_det.Add(en_names);
                            en_det.Add(lat);
                            en_det.Add(lon);
                            en_det.Add(info1);
                            en_det.Add(info2);
                            en_det.Add(info3);
                            en_det.Add(id);

                        }
                        
                    }
                    if (set == 1)
                    {
                        if (subset == 0)
                        {
                            en_det = GetContacts(serverconfig);
                        }
                        if (subset == 1)
                        {
                            var queryContacts = from c in svcContext.ContactSet
                                                where c.CreditLimit.Value > 10000
                                                select new Contact
                                                {
                                                    FirstName = c.FirstName,
                                                    Address1_Latitude = c.Address1_Latitude,
                                                    Address1_Longitude = c.Address1_Longitude,
                                                    EMailAddress1 = c.EMailAddress1,
                                                    Address1_City = c.Address1_City,
                                                    Address1_Country = c.Address1_Country,
                                                    ContactId=c.ContactId
                                                };

                            foreach (var c in queryContacts)
                            {
                                en_names.Add(c.FirstName);
                                lat.Add(c.Address1_Latitude);
                                lon.Add(c.Address1_Longitude);
                                info1.Add(c.EMailAddress1);
                                info2.Add(c.Address1_City);
                                info3.Add(c.Address1_Country);
                                id.Add(c.ContactId);
                            }
                            en_det.Add(en_names);
                            en_det.Add(lat);
                            en_det.Add(lon);
                            en_det.Add(info1);
                            en_det.Add(info2);
                            en_det.Add(info3);
                            en_det.Add(id);
                        }
                        if (subset == 2)
                        {
                            var queryContacts = from c in svcContext.ContactSet
                                                where c.CreditLimit.Value < 10000
                                                select new Contact
                                                {
                                                    FirstName = c.FirstName,
                                                    Address1_Latitude = c.Address1_Latitude,
                                                    Address1_Longitude = c.Address1_Longitude,
                                                    EMailAddress1 = c.EMailAddress1,
                                                    Address1_City = c.Address1_City,
                                                    Address1_Country = c.Address1_Country,
                                                    ContactId = c.ContactId
                                                };

                            foreach (var c in queryContacts)
                            {
                                en_names.Add(c.FirstName);
                                lat.Add(c.Address1_Latitude);
                                lon.Add(c.Address1_Longitude);
                                info1.Add(c.EMailAddress1);
                                info2.Add(c.Address1_City);
                                info3.Add(c.Address1_Country);
                                id.Add(c.ContactId);
                            }
                            en_det.Add(en_names);
                            en_det.Add(lat);
                            en_det.Add(lon);
                            en_det.Add(info1);
                            en_det.Add(info2);
                            en_det.Add(info3);
                            en_det.Add(id);
                        }

                    }
                    if (set == 2)
                    {
                        if (subset == 0)
                        {
                            en_det = GetLeads(serverconfig);
                        }

                        if (subset == 1)
                        {
                        }
                        if (subset == 2)
                        {
                        }

                    }

                    return en_det;
                }
                
            }
            catch (FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault>)
            {
                throw;
            }
        }

        public void accUpdate(Microsoft.Crm.Sdk.Samples.ServerConnection.Configuration serverconfig, ArrayList data)
        {
            try
            {
                using (_serviceProxy = Microsoft.Crm.Sdk.Samples.ServerConnection.GetOrganizationProxy(serverconfig))
                {
                  
                    String a_id = (String)data[0];
                    Guid _accountId = new Guid(a_id);
                    _serviceProxy.EnableProxyTypes();

                    _service = (IOrganizationService)_serviceProxy;
 
                    Account accountToUpdate = new Account
                    {
                        Name = (String)data[1],
                        EMailAddress1= (String)data[2],
                        Address1_City= (String)data[3],
                        Address1_Country= (String)data[4],
                        Address1_Latitude= Convert.ToDouble(data[5]),
                        Address1_Longitude= Convert.ToDouble(data[6]),
                        AccountId = _accountId
                    };
              
                    _service.Update(accountToUpdate);

               
                }
            }
            catch (FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault>)
            {
                throw;
            }
         
        }

        public void conUpdate(Microsoft.Crm.Sdk.Samples.ServerConnection.Configuration serverconfig, ArrayList data)
        {
            try
            {
                using (_serviceProxy = Microsoft.Crm.Sdk.Samples.ServerConnection.GetOrganizationProxy(serverconfig))
                {

                    String a_id = (String)data[0];
                    Guid _contactId = new Guid(a_id);
                    _serviceProxy.EnableProxyTypes();

                    _service = (IOrganizationService)_serviceProxy;

                    Contact contactToUpdate = new Contact
                    {
                        FirstName = (String)data[1],
                        EMailAddress1 = (String)data[2],
                        Address1_City = (String)data[3],
                        Address1_Country = (String)data[4],
                        Address1_Latitude = Convert.ToDouble(data[5]),
                        Address1_Longitude = Convert.ToDouble(data[6]),
                        ContactId = _contactId
                    };

                    _service.Update(contactToUpdate);


                }
            }
            catch (FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault>)
            {
                throw;
            }

        }

        public void leadUpdate(Microsoft.Crm.Sdk.Samples.ServerConnection.Configuration serverconfig, ArrayList data)
        {
            try
            {
                using (_serviceProxy = Microsoft.Crm.Sdk.Samples.ServerConnection.GetOrganizationProxy(serverconfig))
                {

                    String a_id = (String)data[0];
                    Guid _leadId = new Guid(a_id);
                    _serviceProxy.EnableProxyTypes();

                    _service = (IOrganizationService)_serviceProxy;

                    Lead leadToUpdate = new Lead
                    {
                        FirstName = (String)data[1],
                        EMailAddress1 = (String)data[2],
                        Address1_City = (String)data[3],
                        Address1_Country = (String)data[4],
                        Address1_Latitude = Convert.ToDouble(data[5]),
                        Address1_Longitude = Convert.ToDouble(data[6]),
                        LeadId = _leadId
                    };

                    _service.Update(leadToUpdate);


                }
            }
            catch (FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault>)
            {
                throw;
            }

        }
    }
    }

