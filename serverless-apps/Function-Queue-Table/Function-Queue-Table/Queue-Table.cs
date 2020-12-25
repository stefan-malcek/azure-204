using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace Function_Queue_Table
{
    public static class Function1
    {
        [FunctionName("QueueTable")]
        // The ICollector interface is to write multiple to the output
        public static void Run([QueueTrigger("appqueue", Connection = "storage_connection")]JObject myQueueItem,
            [Table("Course", Connection = "storage_connection")]ICollector<Course> outputTable,
            [Queue("newqueue", Connection = "storage_connection")]ICollector<Course> outputQueue,
            [Blob("data/{rand-guid}", FileAccess.Write, Connection = "storage_connection")] TextWriter blobOutput,
            ILogger log)
        {
            log.LogInformation("Adding Course");
            Course obj = new Course
            {
                PartitionKey = myQueueItem["Id"].ToString(),
                RowKey = myQueueItem["Name"].ToString(),
                Rating = myQueueItem["Rating"].Value<double>()
            };
            outputTable.Add(obj); // Use ICollector<T>
            outputQueue.Add(obj); // The output can be an object serializable as JSON, string, byte[] and CloudQueueMessage
            blobOutput.Write($"Partition Key {obj.PartitionKey}");
            // For blob, you have an output of Stream, string, CloudBlockBlob
        }
    }
}
