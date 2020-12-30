using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using cosmos_adddata;
using LumenWorks.Framework.IO.Csv;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace eventhubs_send
{
    class Program
    {
        private static string connstring = "";
        private static string hubname = "apphub";
        static DataTable dt_table;
        static void Main(string[] args)
        {
            LoadData();
            SendData().Wait();
        }
        private static void LoadData()
        {
            dt_table = new DataTable();
            using (var csvReader = new CsvReader(new StreamReader(File.OpenRead("QueryResult.csv")), true))
            {
                dt_table.Load(csvReader);
            }
        }

        private static async Task SendData()
        {
            EventHubProducerClient client = new EventHubProducerClient(connstring, hubname);

            foreach (DataRow row in dt_table.Rows)
            {
                ActivityData obj = new ActivityData();
                obj.Correlationid = row[0].ToString();
                obj.Operationname = row[1].ToString();
                obj.status = row[2].ToString();
                obj.EventCategory = row[3].ToString();
                obj.Level = row[4].ToString();
                obj.dttime = DateTime.Parse(row[5].ToString());
                obj.subscription = row[6].ToString();
                obj.InitiatedBy = row[7].ToString();
                obj.resourcetype = row[8].ToString();
                obj.resourcegroup = row[9].ToString();
                obj.resource = row[10].ToString();
                obj.id = Guid.NewGuid().ToString();

                using EventDataBatch batch_obj = await client.CreateBatchAsync();
                batch_obj.TryAdd(new EventData(Encoding.UTF8.GetBytes(obj.ToString())));
                await client.SendAsync(batch_obj);

                Console.WriteLine("Sending Data {0}", obj.id);
            }
        }
    }
}
