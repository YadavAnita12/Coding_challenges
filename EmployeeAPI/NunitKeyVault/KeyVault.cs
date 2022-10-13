using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NunitKeyVault.Services;

namespace NunitKeyVault
{
    public class KeyVault : IKeyVault
    {
        private readonly IConfiguration _configuration;
        private readonly IkeyVaultService _IkeyVaultServices;
        private readonly ILogger<KeyVault> _logger;


        //public KeyVault(IConfiguration configuration, IkeyVaultService ikeyVaultService, ILogger<KeyVault> logger)
        //{
        //    _configuration = configuration;
        //    _IkeyVaultServices = ikeyVaultService;
        //    _logger = logger;
        //}

        //public Dictionary<String, String>[] GetSecretList(string strEnv, string ClientID, string ClientSecret, string keyVaultUrl)
        //{
        //    try
        //    {
        //        var SecretsList = _IkeyVaultServices.GetSecretAsync(strEnv, ClientID, ClientSecret, keyVaultUrl).Result;
        //        return ConvertDictionary(SecretsList);
        //    }
        //    catch (Exception ex)
        //    {
        //        var strckvar = ex.StackTrace.ToString() + " Error Message : " + ex.Message;
        //        _logger.LogError(strckvar);
        //        throw ex;
        //    }

        //}

        //public Dictionary<string, string>[] ConvertDictionary(List<string> SecretsList)
        //{
        //    Dictionary<String, String> arrKeys_1 = new Dictionary<String, String>();
        //    Dictionary<String, String>[] arrOfDictionaryKeys = new Dictionary<String, String>[SecretsList.Count];
        //    int count = 0;
        //    foreach (var secretKey in SecretsList)
        //    {
        //        var displayNameForSecret = "";
        //        Dictionary<String, String> arrKeys = new Dictionary<String, String>();
        //        var arrDisplayName = secretKey.Split('-');
        //        if (arrDisplayName.Length < 2) throw new Exception("KeyVault keys are not in correct format.");
        //        displayNameForSecret = arrDisplayName[0];
        //        arrKeys.Add("label", displayNameForSecret);
        //        arrKeys.Add("value", secretKey);
        //        arrOfDictionaryKeys[count] = arrKeys;
        //        count++;
        //        arrKeys_1 = arrKeys;
        //    }
        //    return (arrOfDictionaryKeys);
        //}

    }
}
