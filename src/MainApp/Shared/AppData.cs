using Camunda.Http.Api;
using Camunda.Http.Api.Client;
using MudBlazor;
using System.Net.Http;
using System;

namespace MainApp.Shared
{
    public class AppData
        {

        public CamundaClient CamundaClient { get; set; }

        public string EngineUrl { get; set; } = "https://localhost:8080/engine-rest";

        public string EngineUsername { get; set; } = "admin";

        public string EnginePassword { get; set; } = "admin";

        private readonly IHttpClientFactory _clientFactory;
        
        public HttpClient HttpClient { get; set; }
        
        public AppData(IHttpClientFactory clientFactory)
        {
            this._clientFactory = clientFactory;
            CreateClient();
        }

        public string GetBasicAuthValue(string username, string password)
        {
            string encoded = Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(EngineUsername + ":" + EnginePassword));
            return "Basic " + encoded;
        }

        public void CreateClient()
        {
            var client = this._clientFactory.CreateClient("CamundaAPI");
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

        public void SnackError(ISnackbar snackbar, string message, Exception e = null, bool throwError = false)
        {
            snackbar.Add(message, Severity.Error);
            Console.Error.WriteLine(message);
            if (e != null && throwError)
            {
                throw e;
            }
        }

        public void SnackNormal(ISnackbar snackbar, string message)
        {
            snackbar.Add(message);
        }
        
        public void SnackSuccess(ISnackbar snackbar, string message)
        {
            snackbar.Add(message, Severity.Success);
        }
        
        public void SnackWarning(ISnackbar snackbar, string message)
        {
            snackbar.Add(message, Severity.Warning);
        }
        
    }
}
