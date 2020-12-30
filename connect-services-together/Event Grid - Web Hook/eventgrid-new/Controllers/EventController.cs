using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.EventGrid.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace eventgrid_new.Controllers
{
    public class EventController : Controller
    {

        [HttpGet]
        public string Index()
        {
            return "Hello";
        }

        [HttpPost]
        public IActionResult Index([FromBody]object request)
        {
            var EventGrid = JsonConvert.DeserializeObject<EventGridEvent[]>(request.ToString()).FirstOrDefault();

            // First EventGrid will send a ValidationEvent for creating a handshake.
            if (string.Equals(EventGrid.EventType, "Microsoft.EventGrid.SubscriptionValidationEvent", StringComparison.OrdinalIgnoreCase))
            {
                var data = EventGrid.Data as JObject;
                if (data != null)
                {
                    var eventData = data.ToObject<SubscriptionValidationEventData>();
                    var responseData = new SubscriptionValidationResponse
                    {
                        ValidationResponse = eventData.ValidationCode
                    };
                    if (responseData.ValidationResponse != null)
                    {
                        // Handshake between EventGrid and web hook.
                        // Web App has to send the validation code back to the Azure service.
                        return Ok(responseData);
                    }
                }
                else
                {
                    return Ok();
                }
            }
            else
            {
                var data = EventGrid.Data as JObject;
                string topic = EventGrid.Topic;
                foreach (JProperty property in data.Properties())
                {
                    string name = property.Name.ToString();
                    string val = property.Value.ToString(); ;
                }
                return Ok();
            }

            return Ok();

        }
    }
}