using Camunda.Http.Api;
using Camunda.Http.Api.Client;
using Microsoft.AspNetCore.Components;
using MudBlazor;
    


namespace MainApp.Shared
{
    using System.Net.Http;
    using System;

    public class AppData
        {

        public CamundaClient CamundaClient { get; set; }

        public string EngineUrl { get; set; } = "https://localhost:8080/engine-rest";

        public string EngineUsername { get; set; } = "admin";

        public string EnginePassword { get; set; } = "admin";

        private readonly IHttpClientFactory ClientFactory;
        

        public HttpClient HttpClient { get; set; }

        [Inject]
        private ISnackbar Snackbar { get; set; }


        public AppData(IHttpClientFactory clientFactory)
        {
            this.ClientFactory = clientFactory;
            CreateClient();
        }

        public string GetBasicAuthValue(string username, string password)
        {
            string encoded = Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(EngineUsername + ":" + EnginePassword));
            return "Basic " + encoded;
        }

        public void CreateClient()
        {
            var client = this.ClientFactory.CreateClient("CamundaAPI");
            var config = new Configuration
            {
                BasePath = EngineUrl
            };
            var handler = new HttpClientHandler();
            
            // client.BaseAddress = new Uri(EngineUrl);

            if (!String.IsNullOrEmpty(EngineUsername))
            {
                this.CamundaClient = new CamundaClient(config, client, handler, EngineUsername, EnginePassword);
                // client.DefaultRequestHeaders.Add("Authorization", GetBasicAuthValue(EngineUsername, EnginePassword));
            }
            else
            {
                this.CamundaClient = new CamundaClient(config, client, handler);
            }
        }

        public void SnackError(string message, Exception e = null)
        {
            Snackbar.Add(message, Severity.Error);
            Console.Error.WriteLine(message);
            if (e != null)
            {
                throw e;
            }
        }

        public void SnackNormal(string message)
        {
            
            Snackbar.Add(message);
        }
        
        public void SnackSuccess(string message)
        {
            Snackbar.Add(message, Severity.Success);
        }
        
        public void SnackWarning(string message)
        {
            Snackbar.Add(message, Severity.Warning);
        }
        
    }
}
