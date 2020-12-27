using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using System;

namespace KeyVault
{
    class Program
    {
        /// <summary>
        /// 1. Login into the Azure portal: az login
        /// 2. Create a service principal in Azure AD: az ad sp create-for-rbac -n secretapp --skip-assignment
        /// 3. Assign rights to key vault: az keyvault set-policy --name demovault5001 --spn "<appId>" --secret-permissions backup delete get list set
        /// 4. Set System environment variables to store AZURE_CLIENT_ID, AZURE_CLIENT_SECRET and AZURE_TENANT_ID
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string keyVaultUrl = "https://demovault5001.vault.azure.net/";
            var client = new SecretClient(vaultUri: new Uri(keyVaultUrl), credential: new DefaultAzureCredential());
                        
            KeyVaultSecret secret = client.GetSecret("dbpassword");
            Console.WriteLine(secret.Value);
            Console.ReadKey();
        }
    }
}
