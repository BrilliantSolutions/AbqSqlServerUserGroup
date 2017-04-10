using Salesforce.Common;
using Salesforce.Force;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseSoapApi
{
    public class ThornburgForceClient
    {
        private static readonly string securityToken = ConfigurationManager.AppSettings["SecurityToken"];
        private static readonly string consumerKey = ConfigurationManager.AppSettings["ConsumerKey"];
        private static readonly string consumerSecret = ConfigurationManager.AppSettings["ConsumerSecret"];
        private static readonly string username = ConfigurationManager.AppSettings["Username"];
        private static readonly string password = ConfigurationManager.AppSettings["Password"] + securityToken;
        private static readonly string isSandboxUser = ConfigurationManager.AppSettings["IsSandboxUser"];
        private static readonly string tokenRequestEndpointProductionUrl = ConfigurationManager.AppSettings["TokenRequestEndpointProductionUrl"];
        private static readonly string tokenRequestEndpointSandboxUrl = ConfigurationManager.AppSettings["TokenRequestEndpointSandboxUrl"];

        public ThornburgForceClient()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        public static async Task<ForceClient> GetForceClient()
        {
            var auth = new AuthenticationClient();

            // Authenticate with Salesforce
            Console.WriteLine(value: "Authenticating with Salesforce");
            string tokenRequestEndpointUrl = isSandboxUser.Equals(value: "true", comparisonType: StringComparison.CurrentCultureIgnoreCase)
                ? tokenRequestEndpointSandboxUrl
                : tokenRequestEndpointProductionUrl;

            await auth.UsernamePasswordAsync(consumerKey, consumerSecret, username, password, tokenRequestEndpointUrl);
            Console.WriteLine(value: "Connected to Salesforce");

            return new ForceClient(auth.InstanceUrl, auth.AccessToken, auth.ApiVersion);
        }


    }
}
