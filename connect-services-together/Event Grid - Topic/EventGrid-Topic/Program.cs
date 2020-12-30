using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EventGrid_Topic
{
    class Program
    {
        static string _endpoint = "https://apptopiccustomer.westeurope-1.eventgrid.azure.net/api/events";
        static string _key = "";
        static async Task Main(string[] args)
        {
            HttpClient _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("aeg-sas-key", _key);

            customer obj = new customer(1, "John");
            GridEvent evnt = new GridEvent();
            evnt.Id = Guid.NewGuid().ToString();
            evnt.Data = obj;
            evnt.EventTime = DateTime.UtcNow;
            evnt.Subject = "Customer/New";            
            evnt.EventType = "NewCustomer";

            var events = new List<GridEvent>();
            events.Add(evnt);
            string _json_data = JsonConvert.SerializeObject(events);
            HttpRequestMessage _request = new HttpRequestMessage(HttpMethod.Post, _endpoint)
            {
                Content = new StringContent(_json_data, Encoding.UTF8, "application/json")
            };

            HttpResponseMessage _response = await _client.SendAsync(_request);
            
            Console.WriteLine("Event Sent");
            Console.WriteLine(_response.Content.ReadAsStringAsync().Result.ToString());
            Console.ReadLine();
        }
    }
}
