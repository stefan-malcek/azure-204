using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace queue_send
{
    class Program
    {
        private static string _bus_connectionstring = "";
        private static string _queue_name = "appqueue";
        static async Task Main(string[] args)
        {
            IQueueClient _client;
            _client = new QueueClient(_bus_connectionstring, _queue_name);
            Console.WriteLine("Sending Messages");
            for (int i = 1; i <= 10; i++)
            {
                Order obj = new Order();
                var message = new Message(Encoding.UTF8.GetBytes(obj.ToString()));

                // Setting TTL for the message will override the queue TTL.
                message.TimeToLive = TimeSpan.FromSeconds(240);

                message.MessageId = obj.Id;
                message.UserProperties.Add("Quantity", obj.quantity);
                
                await _client.SendAsync(message);
                Console.WriteLine($"Sending Message : {obj.Id}");
            }
        }
    }
}
