using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HelperServices
{
    public class HangfireJobService: IHangfireJobService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<HangfireJobService> _logger;
        
        public HangfireJobService(IHttpClientFactory clientFactory, ILogger<HangfireJobService> logger)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }
        public async Task ExecuteHangfireJob(string api)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, api);
            request.Headers.Add("Accept", "application/vnd.github.v3+json");
            request.Headers.Add("User-Agent", "HttpClientFactory-Sample");

            HttpClient client = _clientFactory.CreateClient();
            _logger.LogInformation("Calling API:{0}", api);

            var response =  await client.SendAsync(request);
            string responseBody = await response.Content.ReadAsStringAsync();
            _logger.LogInformation("Response from API:{0} - {1}", response.StatusCode, responseBody);

            await Task.CompletedTask;
        }
    }

    public interface IHangfireJobService
    {
        public Task ExecuteHangfireJob(string api);
    }
}
