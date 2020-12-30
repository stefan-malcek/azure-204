using Microsoft.Azure.ServiceBus;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace queue_receive
{
    class Program
    {
        private static string _bus_connectionstring = "";
        private static string _queue_name = "appqueue";
        private static QueueClient _client;

        static void Main(string[] args)
        {
            QueueFunction().GetAwaiter().GetResult();
        }

        static async Task QueueFunction()
        {
            // Set ReceiveMode.ReceiveAndDelete for deleting messages from queue after receiving.
            _client = new QueueClient(_bus_connectionstring, _queue_name);

            var _options = new MessageHandlerOptions(ExceptionReceived)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            _client.RegisterMessageHandler(Process_Message, _options);
            Console.ReadKey();
        }


        static async Task Process_Message(Message message, CancellationToken token)
        {
            Console.WriteLine($"Message ID : {message.MessageId}");
            Console.WriteLine($"Sequence Number ID : {message.SystemProperties.SequenceNumber}");
            message.UserProperties.TryGetValue("Quantity", out object quantity);
            Console.WriteLine($"Quantity : {(int)quantity}");

            // Do not call when ReceiveMode.ReceiveAndDelete is set.
            await _client.CompleteAsync(message.SystemProperties.LockToken);
        }

        static Task ExceptionReceived(ExceptionReceivedEventArgs args)
        {
            Console.WriteLine(args.Exception);
            return Task.CompletedTask;
        }

    }
}
