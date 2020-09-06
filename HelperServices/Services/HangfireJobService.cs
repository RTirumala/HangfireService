using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace HelperServices.Services
{
    public class HangfireJobService: IHangfireJobService
    {
        private readonly IHttpClientFactory _clientFactory;
        public HangfireJobService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public void ExecuteHangfireJob(string api)
        {
            HttpClient client = new HttpClient();
        }
    }

    public interface IHangfireJobService
    {
        public void ExecuteHangfireJob(string api);
    }
}
