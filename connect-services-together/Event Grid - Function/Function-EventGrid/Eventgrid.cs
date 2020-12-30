using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;

namespace Function_EventGrid
{
    public static class Eventgrid
    {
        [FunctionName("Eventgrid")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {                     
            string response = string.Empty;

            string requestContent = await new StreamReader(req.Body).ReadToEndAsync();

            EventGridSubscriber eventGridSubscriber = new EventGridSubscriber();

            EventGridEvent[] eventGridEvents = eventGridSubscriber.DeserializeEventGridEvents(requestContent);

            foreach (EventGridEvent eventGridEvent in eventGridEvents)
            {
                if (eventGridEvent.Data is SubscriptionValidationEventData)
                {
                    var eventData = (SubscriptionValidationEventData)eventGridEvent.Data;
                    log.LogInformation($"Got SubscriptionValidation event data, validation code: {eventData.ValidationCode}, topic: {eventGridEvent.Topic}");

                    var responseData = new SubscriptionValidationResponse()
                    {
                        ValidationResponse = eventData.ValidationCode
                    };

                    return new OkObjectResult(responseData);
                }
                else 
                {
                    log.LogInformation(eventGridEvent.Data.ToString());
                    
                    log.LogInformation($"EventType {eventGridEvent.EventType}");
                    

                }
            }

            return new OkObjectResult(string.Empty);
        }
    }
}
