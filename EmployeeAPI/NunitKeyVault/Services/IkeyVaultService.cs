using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.KeyVault;

namespace NunitKeyVault.Services
{
    internal interface IkeyVaultService
    {
        string GetSecretAsync(string strEnv, string ClientID, string ClientSecret, string strIndentifier);
        KeyVaultClient GetKeyVaultConnection(string strEnv, string ClientID, string ClientSecret);
        Task<List<String>> GetSecretsAsync(string strEnv, string ClientID, string ClientSecret, string keyVaultUrl);

    }
}
