


using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Crm.Sdk.Messages;
using System.Linq;
using System.Collections.Generic;

namespace Microsoft.Crm.Sdk.Samples
{
   
 ///   At run-time, you will be given the option to delete all the
    /// database records created by this program.
    public class CRUDOperations
    {
        #region Class Level Members

        private Guid _accountId;
        private OrganizationServiceProxy _serviceProxy;
        private IOrganizationService _service;

        #endregion Class Level Members

        #region How To Sample Code
        /// <summary>
        /// This method first connects to the Organization service. Afterwards,
        /// basic create, retrieve, update, and delete entity operations are performed.
        /// </summary>
        /// <param name="serverConfig">Contains server connection information.</param>
        /// <param name="promptforDelete">When True, the user will be prompted to delete all
        /// created entities.</param>
        public void Run(ServerConnection.Configuration serverConfig, bool promptforDelete)
        {
            try
            {
                //<snippetCRUDOperations1>
                // Connect to the Organization service. 
                // The using statement assures that the service proxy will be properly disposed.
                using (_serviceProxy = ServerConnection.GetOrganizationProxy(serverConfig) )
                {
                    _serviceProxy.EnableProxyTypes();
                    _service = (IOrganizationService)_serviceProxy;

                    CreateRequiredRecords();
                    ServiceContext svcContext = new ServiceContext(_service);

                    var accounts = from a in svcContext.AccountSet
                                   select new Account
                                   {
                                       Name = a.Name,
                                       Address1_County = a.Address1_County
                                   };
                    System.Console.WriteLine("List all accounts in CRM");
                    System.Console.WriteLine("========================");
                    foreach (var a in accounts)
                    {
                        System.Console.WriteLine(a.Name + " " + a.Address1_County);
                    }
                    //</snippetCreateALinqQuery1>
                    System.Console.WriteLine();
                    System.Console.WriteLine("<End of Listing>");
                    System.Console.WriteLine();



                    var queryContacts = from c in svcContext.ContactSet
                                        select new Contact
                                        {
                                            FirstName = c.FirstName,
                                            LastName = c.LastName,
                                            Address1_City = c.Address1_City
                                        };
                    System.Console.WriteLine("List all contacts in CRM");
                    System.Console.WriteLine("=====================================");
                    foreach (var c in queryContacts)
                    {
                        System.Console.WriteLine(c.FirstName + " " +
                            c.LastName + " " + c.Address1_City);
                    }
                   
                    System.Console.WriteLine();
                    System.Console.WriteLine("<End of Listing>");
                    System.Console.WriteLine();

                   
                    
                    
                    // Display information about the logged on user.
                    Guid userid = ((WhoAmIResponse)_serviceProxy.Execute(new WhoAmIRequest())).UserId;
                    SystemUser systemUser = (SystemUser)_serviceProxy.Retrieve("systemuser", userid, 
                        new ColumnSet(new string[] {"firstname", "lastname"}));
                    Console.WriteLine("Logged on user is {0} {1}.", systemUser.FirstName, systemUser.LastName);
 
                    // Retrieve the version of Microsoft Dynamics CRM.
                    RetrieveVersionRequest versionRequest = new RetrieveVersionRequest();
                    RetrieveVersionResponse versionResponse =
                        (RetrieveVersionResponse)_serviceProxy.Execute(versionRequest);
                    Console.WriteLine("Microsoft Dynamics CRM version {0}.", versionResponse.Version);

                    //<snippetCRUDOperations2>

                    // Instantiate an account object. Note the use of the option set enumerations defined in OptionSets.cs.
                    // See the Entity Metadata topic in the SDK documentation to determine 
                    // which attributes must be set for each entity.
                    Account account = new Account { Name = "Fourth Coffee" };
                    account.AccountCategoryCode = new OptionSetValue((int)AccountAccountCategoryCode.PreferredCustomer);
                    account.CustomerTypeCode = new OptionSetValue((int)AccountCustomerTypeCode.Investor);

                    // Create an account record named Fourth Coffee.
                    _accountId = _serviceProxy.Create(account);

                    //</snippetCRUDOperations2>
                    Console.Write("{0} {1} created, ", account.LogicalName, account.Name);

                    // Retrieve the account containing several of its attributes.
                    ColumnSet cols = new ColumnSet(
                        new String[] { "name", "address1_postalcode", "lastusedincampaign" });

                    Account retrievedAccount = (Account)_serviceProxy.Retrieve("account", _accountId, cols);
                    Console.Write("retrieved, ");

                    // Update the postal code attribute.
                    retrievedAccount.Address1_PostalCode = "98052";

                    // The address 2 postal code was set accidentally, so set it to null.
                    retrievedAccount.Address2_PostalCode = null;

                    // Shows use of a Money value.
                    retrievedAccount.Revenue = new Money(5000000);

                    // Shows use of a Boolean value.
                    retrievedAccount.CreditOnHold = false;

                    // Update the account record.
                    _serviceProxy.Update(retrievedAccount);
                    Console.WriteLine("and updated.");

                    

                    DeleteRequiredRecords(promptforDelete);
                }
                //</snippetCRUDOperations1>
            }

            // Catch any service fault exceptions that Microsoft Dynamics CRM throws.
            catch (FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault>)
            {
                // You can handle an exception here or pass it back to the calling method.
                throw;
            }
        }

        /// <summary>
        /// Creates any entity records that this sample requires.
        /// </summary>
        public void CreateRequiredRecords()
        {
            // For this sample, all required entities are created in the Run() method.
        }
      
        /// <summary>
        /// Deletes any entity records that were created for this sample.
        /// <param name="prompt">Indicates whether to prompt the user 
        /// to delete the records created in this sample.</param>
        /// </summary>
        public void DeleteRequiredRecords(bool prompt)
        {
            bool deleteRecords = true;

            if (prompt)
            {
                Console.WriteLine("\nDo you want these entity records deleted? (y/n) [y]: ");
                String answer = Console.ReadLine();

                deleteRecords = (answer.StartsWith("y") || answer.StartsWith("Y") || answer == String.Empty);
            }

            if (deleteRecords)
            {
                _serviceProxy.Delete(Account.EntityLogicalName, _accountId);
                Console.WriteLine("Entity records have been deleted.");
            }
        }
      
        #endregion How To Sample Code

        #region Main method

        /// <summary>
        /// Standard Main() method used by most SDK samples.
        /// </summary>
        /// <param name="args"></param>
        static public void Main(string[] args)
        {
            try
            {
                string u = "hemantsom@hotmail.com";
                string p = "som020191";
                int a = 0;

                ServerConnection serverConnect = new ServerConnection();
                ServerConnection.Configuration config = serverConnect.GetServerConfiguration(u,p,a);

                CRUDOperations app = new CRUDOperations();
                app.Run( config, true );
            }

            //<snippetCRUDOperations3>
            catch (FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault> ex)
            {
                Console.WriteLine("The application terminated with an error.");
                Console.WriteLine("Timestamp: {0}", ex.Detail.Timestamp);
                Console.WriteLine("Code: {0}", ex.Detail.ErrorCode);
                Console.WriteLine("Message: {0}", ex.Detail.Message);
                Console.WriteLine("Trace: {0}", ex.Detail.TraceText);
                Console.WriteLine("Inner Fault: {0}",
                    null == ex.Detail.InnerFault ? "Has Inner Fault" : "No Inner Fault");
            }
            catch (System.TimeoutException ex)
            {
                Console.WriteLine("The application terminated with an error.");
                Console.WriteLine("Message: {0}", ex.Message);
                Console.WriteLine("Stack Trace: {0}", ex.StackTrace);
                Console.WriteLine("Inner Fault: {0}",
                    null == ex.InnerException.Message ? "Has Inner Fault" : "No Inner Fault");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("The application terminated with an error.");
                Console.WriteLine(ex.Message);

                // Display the details of the inner exception.
                if (ex.InnerException != null)
                {
                    Console.WriteLine(ex.InnerException.Message);

                    FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault> fe = ex.InnerException
                        as FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault>;
                    if (fe != null)
                    {
                        Console.WriteLine("Timestamp: {0}", fe.Detail.Timestamp);
                        Console.WriteLine("Code: {0}", fe.Detail.ErrorCode);
                        Console.WriteLine("Message: {0}", fe.Detail.Message);
                        Console.WriteLine("Trace: {0}", fe.Detail.TraceText);
                        Console.WriteLine("Inner Fault: {0}",
                            null == fe.Detail.InnerFault ? "Has Inner Fault" : "No Inner Fault");
                    }
                }
            }
            //</snippetCRUDOperations3>
            // Additional exceptions to catch: SecurityTokenValidationException, ExpiredSecurityTokenException,
            // SecurityAccessDeniedException, MessageSecurityException, and SecurityNegotiationException.

            finally
            {
                Console.WriteLine("Press <Enter> to exit.");
                Console.ReadLine();
            }
        }
        #endregion Main method
    }
}
//</snippetCRUDOperations>
