using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace NunitKeyVault.Services
{
    internal class keyVaultService
    {

        private readonly IConfiguration configuration;
        private readonly ILogger<keyVaultService> _logger;
        public keyVaultService(IConfiguration config, ILogger<keyVaultService> logger)
        {
            configuration = config;
            _logger = logger;
        }
        public string AWP_GetSecretAsync(string strEnv, string ClientID, string ClientSecret, string strIndentifier)
        {
            try
            {
                KeyVaultClient client = GetKeyVaultConnection(strEnv, ClientID, ClientSecret);
                var secret = client.GetSecretAsync(strIndentifier).Result;
                return secret.Value;
            }
            catch (Exception ex)
            {
                var strckvar = ex.StackTrace.ToString() + " Error Message : " + ex.Message;
                _logger.LogError(strckvar);
                throw ex;
            }
        }

        public KeyVaultClient GetKeyVaultConnection(string strEnv, string ClientID, string ClientSecret) //Put this entire funation in  repo 
        {
            try
            {
                KeyVaultClient clientVault = null;

                //if Environment Variable is All then using Managed Identity
                if (string.Equals(strEnv, "KeyVault", StringComparison.OrdinalIgnoreCase))
                {
                    AzureServiceTokenProvider azureServiceTokenProvider = new AzureServiceTokenProvider();

                    clientVault = new KeyVaultClient(
                    new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
                    //client = clientVault;
                }
                else //by default service principal 
                {
                    clientVault = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(
                               async (string auth, string res, string scope) =>
                               {
                                   var authContext = new AuthenticationContext(auth);
                                   var credential = new ClientCredential(ClientID, ClientSecret);
                                   AuthenticationResult result = await authContext.AcquireTokenAsync(res, credential);
                                   if (result == null)
                                   {
                                       throw new InvalidOperationException("Failed to retrive token");
                                   }
                                   return result.AccessToken;

                               }
                           ));
                }
                return clientVault;
            }
            catch (Exception ex)
            {
                var strckvar = ex.StackTrace.ToString() + " Error Message : " + ex.Message;
                _logger.LogError(strckvar);
                throw ex;
            }
        }

        public async Task<List<String>> GetSecretsAsync(string strEnv, string ClientID, string ClientSecret, string keyVaultUrl)
        {
            try
            {
                KeyVaultClient client = GetKeyVaultConnection(strEnv, ClientID, ClientSecret);
                var secret = await client.GetSecretsAsync(keyVaultUrl);
                //$"{BaseUri}{secretname}"
                //var scrtname= await client.GetSecretAsync(keyVaultUrl);
                //var myId = "";
                List<String> SecretsList = new List<String>();

                foreach (Microsoft.Azure.KeyVault.Models.SecretItem someItem in secret)
                {
                    SecretsList.Add(someItem.Identifier.Name);
                    //myId = someItem.Id;
                    //var mOtherThing = someItem.Identifier;
                    //var yep = await client.GetSecretAsync(mOtherThing.ToString());
                    //SecretsList.Add(yep.Value);
                }

                return SecretsList;
            }
            catch (Exception ex)
            {
                var strckvar = ex.StackTrace.ToString() + " Error Message : " + ex.Message;
                _logger.LogError(strckvar);
                throw ex;
            }
        }

      
    }
}
