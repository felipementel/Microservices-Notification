using IdentityModel.Client;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SportStore.Microservice.Notification.Domain.Model
{
    public class EmailCompose
    {
        public EmailCompose(string userId, string emailBody)
        {
            UserId = userId;
            EmailBody = emailBody;
            EmailDestination = ValidUserId(userId).Result.Email;
        }

        public string UserId { get; set; }

        public string EmailBody { get; private set; }

        public string EmailDestination { get; private set; }

        public string EmailDestinationName { get; private set; }

        private async Task<User> ValidUserId(string userId)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = "https://sportstore-iam-microservice-identity.azurewebsites.net/connect/token",

                ClientId = "NotificationMicroservice_ApiResource",
                //ClientSecret
                //Scope

                UserName = "Admin",
                Password = "Abcd123$"
            });

            if (response.HttpResponse.IsSuccessStatusCode)
            {
                string baseAddress = "https://sportstore-iam-microservice-admin-api.azurewebsites.net";

                HttpClientHandler handler = new HttpClientHandler();
                HttpClient client = new HttpClient(handler);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", response.AccessToken.ToString());

                var APIResponse = await client.GetAsync($"{baseAddress}/api/Users/{userId}");

                if (APIResponse.IsSuccessStatusCode)
                {
                    var JsonContent = await APIResponse.Content.ReadAsStringAsync();
                    User user = JsonConvert.DeserializeObject<User>(JsonContent);

                    return user;
                }
                else
                {
                    throw new Exception("Ocorreu um erro na chamada http.");
                }
            }
            else
            {
                throw new Exception("Não foi possivel validar o UserId");
            }
        }
    }

    public class User
    {
        public string Email { get; set; }
    }
}