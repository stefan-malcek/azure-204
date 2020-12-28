using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TestingWeb_Insights
{
    class Program
    {
        static void Main(string[] args)
        {
            SendRequest().Wait();
            Console.ReadLine();
        }

        static async Task SendRequest()
        {
            string url1 = "https://appinsightsdemo500.azurewebsites.net/products";


            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("UserId", "2");
            while (true)
            {

                var response = client.GetStringAsync(url1);
                Console.WriteLine(response.Result.ToString());
                Thread.Sleep(2000);


            }

        }
    }
}
