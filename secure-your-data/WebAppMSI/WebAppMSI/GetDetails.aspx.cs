using System;
using System.Threading.Tasks;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;

namespace WebAppMSI
{
    public partial class GetDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            OnGetAsync().GetAwaiter().GetResult();
        }

        public async Task OnGetAsync()
        {
            AzureServiceTokenProvider azureServiceTokenProvider = new AzureServiceTokenProvider();
            KeyVaultClient keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
            var secret = await keyVaultClient.GetSecretAsync("https://demovault5001.vault.azure.net/secrets/demosecret")
                    .ConfigureAwait(false);
            Response.Write(secret.Value);
        }
    }
}