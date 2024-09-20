using AVDSupervisorApp.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AVDSupervisorApp.Services
{
    public class HostPoolService
    {
        public async Task<List<string>> GetHostPoolNamesAsync(string token)
        {
            try
            {
                // Construct the URL for the API call
                string apiUrl = $"https://management.azure.com/subscriptions/{Config.SubscriptionId}/providers/Microsoft.DesktopVirtualization/hostPools?api-version={Config.ApiVersion}";

                // Send HTTP GET request to the API endpoint
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode(); // Throw an exception if request is not successful

                    // Deserialize JSON response
                    string json = await response.Content.ReadAsStringAsync();
                    HostPoolList hostPoolList = JsonConvert.DeserializeObject<HostPoolList>(json);

                    // Extract names of host pools
                    List<string> hostPoolNames = new List<string>();
                    foreach (var hostPool in hostPoolList.Value)
                    {
                        hostPoolNames.Add(hostPool.Name);
                    }

                    return hostPoolNames;
                }
            }
            catch (Exception ex)
            {
                // Handle  exceptions
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        public async Task<List<UserSession>> GetUserSessionsForHostPool(string hostPoolName, string token)
        {
            try
            {

                string apiUrl = $"https://management.azure.com/subscriptions/{Config.SubscriptionId}/resourceGroups/{Config.ResourceGroup}/providers/Microsoft.DesktopVirtualization/hostPools/{hostPoolName}/userSessions?api-version={Config.ApiVersion}";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();

                    string json = await response.Content.ReadAsStringAsync();
                    UserSessionList userSessionList = JsonConvert.DeserializeObject<UserSessionList>(json);

                    return userSessionList?.Value;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

    }

    public class HostPoolList
    {
        public List<HostPool> Value { get; set; }
    }

    public class HostPool
    {
        public string Name { get; set; }
        public string Id { get; set; }
        // Add properties as needed
    }
}
