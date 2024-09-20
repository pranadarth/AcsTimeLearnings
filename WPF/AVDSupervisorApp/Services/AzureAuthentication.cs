using AVDSupervisorApp.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using Microsoft.Graph.Models;

namespace AVDSupervisorApp.Services
{
    public class AzureAuthentication
    {
        // Azure AD app settings
        private readonly string _clientId;
        private readonly string _tenantId;
        private readonly string[] _UserReadscopes = { "https://graph.microsoft.com/User.Read" };
        private readonly string[] _AVDscopes = { "https://management.azure.com/.default" };
        private IPublicClientApplication app;

        public AzureAuthentication()
        {

        }

        public async Task<(bool, string)> AuthenticateAsync()
        {
            try
            {
                // Create a public client application with Azure AD app settings
                app = PublicClientApplicationBuilder.Create(Config.ClientId)
                .WithRedirectUri(Config.loginUrl)
                .WithAuthority(AzureCloudInstance.AzurePublic, Config.TenantId)
                 .Build();


                var result = await app.AcquireTokenInteractive(_AVDscopes)
                                      .ExecuteAsync();

                // If authentication succeeds, return true and Token
                return (true, result.AccessToken);
            }
            catch (MsalServiceException)
            {
                // If authentication fails, return false
                return (false, null);
            }
            catch (MsalClientException ms)
            {
                if (!ms.Message.Contains("User canceled"))
                {
                    System.Windows.MessageBox.Show(ms.Message);
                }

                return (false, null);
            }
        }

        public async Task<bool> UserHasAccess()
        {
            var accounts = await app.GetAccountsAsync();
            var result = await app.AcquireTokenSilent(_UserReadscopes, accounts.FirstOrDefault()).ExecuteAsync();

            // Use the obtained token to call Microsoft Graph API
            // Retrieve user's role
            List<Group> userGroups = await GetUserFromGraphApi(result.AccessToken);

            //For eg: Admin, need to change as per client role
            bool isUserHasAccess = userGroups?.Any(group => group.Id == Config.TeamLeadersGroupId) ?? false;

            return isUserHasAccess;
        }

        private async Task<List<Group>> GetUserFromGraphApi(string accessToken)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var response = await httpClient.GetAsync("https://graph.microsoft.com/v1.0/me/memberOf");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var memberOfResponse = JsonConvert.DeserializeObject<MemberOfResponse>(content);
                    return memberOfResponse.Value;
                }
                else
                {
                    throw new Exception($"Failed to retrieve user information from Graph API. Status code: {response.StatusCode}");
                }
            }
        }

        public async void SignOut()
        {
            var accounts = await app.GetAccountsAsync();

            if (accounts.Any())
            {
                try
                {
                    await app.RemoveAsync(accounts.FirstOrDefault());
                }
                catch (MsalException ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }
            }
        }
    }

    public class MemberOfResponse
    {
        [JsonProperty("@odata.context")]
        public string ODataContext { get; set; }

        public List<Group> Value { get; set; }
    }
}
