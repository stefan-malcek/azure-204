using Azure.Identity;
using Azure.Security.KeyVault.Keys;
using Azure.Security.KeyVault.Keys.Cryptography;
using System;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        /// <summary>
        /// 1. Login into the Azure portal: az login
        /// 2. Create a service principal in Azure AD: az ad sp create-for-rbac -n secretapp --skip-assignment
        /// 3. Assign rights to key vault: az keyvault set-policy --name demovault5001 --spn "<appId>" --key-permissions backup delete get list create encrypt decrypt update
        /// 4. Set System environment variables to store AZURE_CLIENT_ID, AZURE_CLIENT_SECRET and AZURE_TENANT_ID
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string keyVaultUrl = "https://demovault5001.vault.azure.net/";
            var client = new KeyClient(vaultUri: new Uri(keyVaultUrl), credential: new DefaultAzureCredential());

            // Getting the Encryption key from the key vault
            KeyVaultKey key = client.GetKey("newkey");

            var cryptoClient = new CryptographyClient(keyId: key.Id, credential: new DefaultAzureCredential());
            byte[] plaintext = Encoding.UTF8.GetBytes("This is sensitive data");

            // Encrypting data
            EncryptResult encryptResult = cryptoClient.Encrypt(EncryptionAlgorithm.RsaOaep256, plaintext);

            //Decrypting data
            DecryptResult decryptResult = cryptoClient.Decrypt(EncryptionAlgorithm.RsaOaep256, encryptResult.Ciphertext);
            Console.WriteLine("Plain text");

            string txt = Encoding.UTF8.GetString(decryptResult.Plaintext);
            Console.WriteLine(txt);
            Console.ReadLine();
        }
    }
}
