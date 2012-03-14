
using System;
using System.Collections.Generic;
using System.ServiceModel.Description;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Security;
using System.Runtime.InteropServices;
using System.DirectoryServices.AccountManagement;
using System.ServiceModel;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Discovery;
using Microsoft.Xrm.Sdk;
using System.Text;

namespace Microsoft.Crm.Sdk.Samples
{
    
    public class ServerConnection
    {
       
        
        public class Configuration
        {
            public String ServerAddress;
            public String OrganizationName;
            public Uri DiscoveryUri;
            public Uri OrganizationUri;
            public Uri HomeRealmUri = null;
            public ClientCredentials DeviceCredentials = null;
            public ClientCredentials Credentials = null;
            public AuthenticationProviderType EndpointType;
            public String UserPrincipalName;
            #region internal members of the class
            internal IServiceManagement<IOrganizationService> OrganizationServiceManagement;
            internal SecurityTokenResponse OrganizationTokenResponse;
            internal Int16 AuthFailureCount = 0;
            #endregion

            public override bool Equals(object obj)
            {
                //Check for null and compare run-time types.
                if (obj == null || GetType() != obj.GetType()) return false;

                Configuration c = (Configuration)obj;

                if (!this.ServerAddress.Equals(c.ServerAddress, StringComparison.InvariantCultureIgnoreCase))
                    return false;
                if (!this.OrganizationName.Equals(c.OrganizationName, StringComparison.InvariantCultureIgnoreCase))
                    return false;
                if (this.EndpointType != c.EndpointType)
                    return false;
                if (this.EndpointType == AuthenticationProviderType.ActiveDirectory)
                {
                    if (!this.Credentials.Windows.ClientCredential.Domain.Equals(
                        c.Credentials.Windows.ClientCredential.Domain, StringComparison.InvariantCultureIgnoreCase))
                        return false;
                    if (!this.Credentials.Windows.ClientCredential.UserName.Equals(
                        c.Credentials.Windows.ClientCredential.UserName, StringComparison.InvariantCultureIgnoreCase))
                        return false;
                }
                else if (this.EndpointType == AuthenticationProviderType.LiveId)
                {
                    if (!this.Credentials.UserName.UserName.Equals(c.Credentials.UserName.UserName,
                        StringComparison.InvariantCultureIgnoreCase))
                        return false;
                    if (!this.DeviceCredentials.UserName.UserName.Equals(
                        c.DeviceCredentials.UserName.UserName, StringComparison.InvariantCultureIgnoreCase))
                        return false;
                    if (!this.DeviceCredentials.UserName.Password.Equals(
                        c.DeviceCredentials.UserName.Password, StringComparison.InvariantCultureIgnoreCase))
                        return false;
                }
                else
                {
                    if (!this.Credentials.UserName.UserName.Equals(c.Credentials.UserName.UserName,
                        StringComparison.InvariantCultureIgnoreCase))
                        return false;
                }
                return true;
            }

            public override int GetHashCode()
            {
                int returnHashCode = this.ServerAddress.GetHashCode()
                    ^ this.OrganizationName.GetHashCode()
                    ^ this.EndpointType.GetHashCode();

                if (this.EndpointType == AuthenticationProviderType.ActiveDirectory)
                    returnHashCode = returnHashCode
                        ^ this.Credentials.Windows.ClientCredential.UserName.GetHashCode()
                        ^ this.Credentials.Windows.ClientCredential.Domain.GetHashCode();
                else if (this.EndpointType == AuthenticationProviderType.LiveId)
                    returnHashCode = returnHashCode
                        ^ this.Credentials.UserName.UserName.GetHashCode()
                        ^ this.DeviceCredentials.UserName.UserName.GetHashCode()
                        ^ this.DeviceCredentials.UserName.Password.GetHashCode();
                else
                    returnHashCode = returnHashCode
                        ^ this.Credentials.UserName.UserName.GetHashCode();

                return returnHashCode;
            }

        }
        public List<Configuration> configurations = null;
        private Configuration config = new Configuration();

        

        #region Static methods
        public static OrganizationServiceProxy GetOrganizationProxy(
            ServerConnection.Configuration serverConfiguration)
        {
            // Obtain the organization service proxy for the Federated, LiveId, and OnlineFederated environments. 
            if (serverConfiguration.OrganizationServiceManagement != null
                && serverConfiguration.OrganizationTokenResponse != null)
            {
                return new OrganizationServiceProxy(
                    serverConfiguration.OrganizationServiceManagement,
                    serverConfiguration.OrganizationTokenResponse);
            }

            if (serverConfiguration.OrganizationServiceManagement == null)
                throw new ArgumentNullException("serverConfiguration.OrganizationServiceManagement");

            // Obtain the organization service proxy for the ActiveDirectory environment.
            return new OrganizationServiceProxy(
                serverConfiguration.OrganizationServiceManagement,
                serverConfiguration.Credentials);

        }
        #endregion Static methods

        
        
        public virtual Configuration GetServerConfiguration()
        {
            Boolean addConfig;
            // Read the configuration from the disk, if it exists, at C:\Users\<username>\AppData\Roaming\CrmServer\Credentials.xml.
            Boolean isConfigExist = ReadConfigurations();
            configurations.Clear();
            addConfig = true;

            if (addConfig)
            {
               config.ServerAddress = "crm.dynamics.com";
                    config.DiscoveryUri = new Uri(String.Format("https://dev.{0}/XRMServices/2011/Discovery.svc", config.ServerAddress));
                    config.OrganizationUri = new Uri(String.Format("https://crmnaorg939c6.api.crm.dynamics.com/XRMServices/2011/Organization.svc"));
                    config.DeviceCredentials = GetDeviceCredentials();
                    config.OrganizationUri = GetOrganizationAddress();
               
               }

            // Set IServiceManagement for the current organization.
            IServiceManagement<IOrganizationService> orgServiceManagement =
                    ServiceConfigurationFactory.CreateManagement<IOrganizationService>(
                    config.OrganizationUri);
            config.OrganizationServiceManagement = orgServiceManagement;

          
                // Set the credentials.
                AuthenticationCredentials authCredentials = new AuthenticationCredentials();
                // If UserPrincipalName exists, use it. Otherwise, set the logon credentials from the configuration.
                
                    authCredentials.ClientCredentials = config.Credentials;
                    if (config.EndpointType == AuthenticationProviderType.LiveId)
                    {
                        authCredentials.SupportingCredentials = new AuthenticationCredentials();
                        authCredentials.SupportingCredentials.ClientCredentials = config.DeviceCredentials;
                    }
               
                AuthenticationCredentials tokenCredentials = orgServiceManagement.Authenticate(authCredentials);

               if (tokenCredentials != null)
                {
                    if (tokenCredentials.SecurityTokenResponse != null)
                        config.OrganizationTokenResponse = tokenCredentials.SecurityTokenResponse;
                }
            

            return config;
        }

       
        public OrganizationDetailCollection DiscoverOrganizations(IDiscoveryService service)
        {
            if (service == null) throw new ArgumentNullException("service");
            RetrieveOrganizationsRequest orgRequest = new RetrieveOrganizationsRequest();
            RetrieveOrganizationsResponse orgResponse =
                (RetrieveOrganizationsResponse)service.Execute(orgRequest);

            return orgResponse.Details;
        }

       
       
        public Boolean ReadConfigurations()
        {
            Boolean isConfigExist = false;

            if (configurations == null)
                configurations = new List<Configuration>();

            if (File.Exists(CrmServiceHelperConstants.ServerCredentialsFile))
            {
                XElement configurationsFromFile = XElement.Load(CrmServiceHelperConstants.ServerCredentialsFile);
                foreach (XElement config in configurationsFromFile.Nodes())
                {
                    Configuration newConfig = new Configuration();
                    var serverAddress = config.Element("ServerAddress");
                    if (serverAddress != null)
                        if (!String.IsNullOrEmpty(serverAddress.Value))
                            newConfig.ServerAddress = serverAddress.Value;
                    var organizationName = config.Element("OrganizationName");
                    if (organizationName != null)
                        if (!String.IsNullOrEmpty(organizationName.Value))
                            newConfig.OrganizationName = organizationName.Value;
                    var discoveryUri = config.Element("DiscoveryUri");
                    if (discoveryUri != null)
                        if (!String.IsNullOrEmpty(discoveryUri.Value))
                            newConfig.DiscoveryUri = new Uri(discoveryUri.Value);
                    var organizationUri = config.Element("OrganizationUri");
                    if (organizationUri != null)
                        if (!String.IsNullOrEmpty(organizationUri.Value))
                            newConfig.OrganizationUri = new Uri(organizationUri.Value);
                    var homeRealmUri = config.Element("HomeRealmUri");
                    if (homeRealmUri != null)
                        if (!String.IsNullOrEmpty(homeRealmUri.Value))
                            newConfig.HomeRealmUri = new Uri(homeRealmUri.Value);

                    var vendpointType = config.Element("EndpointType");
                    if (vendpointType != null)
                        newConfig.EndpointType =
                                RetrieveAuthenticationType(vendpointType.Value);
                    if (config.Element("Credentials").HasElements)
                    {
                        newConfig.Credentials =
                            ParseInCredentials(config.Element("Credentials"), newConfig.EndpointType);
                    }
                    if (newConfig.EndpointType == AuthenticationProviderType.LiveId)
                    {
                        newConfig.DeviceCredentials = GetDeviceCredentials();
                    }
                    var userPrincipalName = config.Element("UserPrincipalName");
                    if (userPrincipalName != null)
                        if (!String.IsNullOrWhiteSpace(userPrincipalName.Value))
                            newConfig.UserPrincipalName = userPrincipalName.Value;
                    configurations.Add(newConfig);
                }
            }

          return isConfigExist;
        }


        #region Protected methods

      protected virtual Uri GetOrganizationAddress()
        {
            using (DiscoveryServiceProxy serviceProxy = GetDiscoveryProxy())
            {
                // Obtain organization information from the Discovery service. 
                if (serviceProxy != null)
                {
                    
                    OrganizationDetailCollection orgs = DiscoverOrganizations(serviceProxy);

                    if (orgs.Count > 0)
                    {
                        Console.WriteLine("\nList of organizations that you belong to:");
                        for (int n = 0; n < orgs.Count; n++)
                        {
                            Console.Write("\n({0}) {1} ({2})\t", n + 1, orgs[n].FriendlyName, orgs[n].UrlName);
                        }

                        Console.Write("\n\nSpecify an organization number (1-{0}) : ", orgs.Count);
                        String input = Console.ReadLine();
                        if (input == String.Empty)
                        {
                            input = "1";
                        }
                        int orgNumber;
                        Int32.TryParse(input, out orgNumber);
                        if (orgNumber > 0 && orgNumber <= orgs.Count)
                        {
                            config.OrganizationName = orgs[orgNumber - 1].FriendlyName;
                            // Return the organization Uri.
                            return new System.Uri(orgs[orgNumber - 1].Endpoints[EndpointType.OrganizationService]);
                        }
                        else
                            throw new Exception("The specified organization does not exist.");
                    }
                    else
                    {
                        Console.WriteLine("\nYou do not belong to any organizations on the specified server.");
                        return new System.Uri(String.Empty);
                    }
                }
                else
                    throw new Exception("An invalid server name was specified.");
            }
        }

       
        protected virtual ClientCredentials GetUserLogonCredentials()
        {
            ClientCredentials credentials = new ClientCredentials();
            String userName;
           // SecureString password;
            String password;
            Boolean isCredentialExist = (config.Credentials != null) ? true : false;
            switch (config.EndpointType)
            {
                case AuthenticationProviderType.LiveId:
                    //Console.Write("\n Enter Live ID: ");
                  
                    userName = (isCredentialExist) ?
                        config.Credentials.UserName.UserName : "hemantsom@hotmail.com";
                    if (string.IsNullOrWhiteSpace(userName))
                    {
                        return null;
                    }

                   // Console.Write("Enter Password: ");
                    //password = ReadPassword();
                    password = "som020191";

                    credentials.UserName.UserName = userName;
                    //credentials.UserName.Password = ConvertToUnsecureString(password);
                    credentials.UserName.Password = password;

                    break;
              
                default:
                    credentials = null;
                    break;
            }
            return credentials;
        }

       
       
        protected virtual ClientCredentials GetDeviceCredentials()
        {
            return Microsoft.Crm.Services.Utility.DeviceIdManager.LoadOrRegisterDevice();
        }

        
        private DiscoveryServiceProxy GetDiscoveryProxy()
        {

            IServiceManagement<IDiscoveryService> serviceManagement =
                        ServiceConfigurationFactory.CreateManagement<IDiscoveryService>(
                        config.DiscoveryUri);

          
            config.EndpointType = serviceManagement.AuthenticationType;

           
            config.Credentials = GetUserLogonCredentials();

            AuthenticationCredentials authCredentials = new AuthenticationCredentials();

                authCredentials = new AuthenticationCredentials();
                authCredentials.ClientCredentials = config.Credentials;

               
                    authCredentials.SupportingCredentials = new AuthenticationCredentials();
                    authCredentials.SupportingCredentials.ClientCredentials = config.DeviceCredentials;
               
                
            AuthenticationCredentials tokenCredentials1 = serviceManagement.Authenticate(authCredentials);
                return new DiscoveryServiceProxy(serviceManagement,tokenCredentials1.SecurityTokenResponse);
         
        }

       
       private AuthenticationProviderType RetrieveAuthenticationType(String authType)
        {
            switch (authType)
            {
                case "ActiveDirectory":
                    return AuthenticationProviderType.ActiveDirectory;
                case "LiveId":
                    return AuthenticationProviderType.LiveId;
                case "Federation":
                    return AuthenticationProviderType.Federation;
                case "OnlineFederation":
                    return AuthenticationProviderType.OnlineFederation;
                default:
                    throw new ArgumentException(String.Format("{0} is not a valid authentication type", authType));
            }
        }
       
       
        private ClientCredentials ParseInCredentials(XElement credentials, AuthenticationProviderType endpointType)
        {
            ClientCredentials result = new ClientCredentials();

            switch (endpointType)
            {
                case AuthenticationProviderType.ActiveDirectory:
                    result.Windows.ClientCredential = new System.Net.NetworkCredential()
                    {
                        UserName = credentials.Element("UserName").Value,
                        Domain = credentials.Element("Domain").Value
                    };
                    break;
                case AuthenticationProviderType.LiveId:
                case AuthenticationProviderType.Federation:
                case AuthenticationProviderType.OnlineFederation:
                    result.UserName.UserName = credentials.Element("UserName").Value;
                    break;
                default:
                    break;
            }

            return result;
        }

        #endregion Private methods

        private static class CrmServiceHelperConstants
        {
            
            public static readonly string ServerCredentialsFile = Path.Combine(
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CrmServer"),
                "Credentials.xml");
        }
       
    }
}

