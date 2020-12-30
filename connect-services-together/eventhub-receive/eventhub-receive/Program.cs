using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace eventhub_receive
{
    class Program
    {
        private static string connstring = "";
        private static string hubname = "apphub";

        static void Main(string[] args)
        {
            GetEvents().Wait();
        }

        static private async Task GetEvents()
        {
            EventHubConsumerClient client = new EventHubConsumerClient("$Default", connstring, hubname);

            var cancellation = new CancellationToken();

            // Get partition and offset.
            //string _partition = (await client.GetPartitionIdsAsync()).First();
            //EventPosition _position = EventPosition.FromSequenceNumber(40);
            //Console.WriteLine("Getting events from a certain position from a particular partition");
            //await foreach (PartitionEvent _recent_event in client.ReadEventsFromPartitionAsync(_partition, _position, cancellation))

            Console.WriteLine("Getting the events");
            await foreach (PartitionEvent Allevent in client.ReadEventsAsync(cancellation))
            {
                EventData event_data = Allevent.Data;
                Console.WriteLine($"Partition Id : {Allevent.Partition.PartitionId}");
                Console.WriteLine(Encoding.UTF8.GetString(event_data.Body.ToArray()));
            }
        }
    }
}
