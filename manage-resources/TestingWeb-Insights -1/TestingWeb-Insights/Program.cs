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
                string url1 = "https://appinsightsdemo500.azurewebsites.net/Products";
                string url2 = "https://appinsightsdemo500.azurewebsites.net/Products/Details/";

            var client = new HttpClient();

            while (true)
            {
                var productsResponse = await client.GetStringAsync(url1);
                Console.WriteLine(productsResponse.ToString());
                Thread.Sleep(2000);

                for (int i = 1; i < 4; i++)
                {
                    var detailsResponse = await client.GetStringAsync(url2 + i);
                    Console.WriteLine(detailsResponse.ToString());
                    Thread.Sleep(1000);

                }


            }

        }
    }
}
