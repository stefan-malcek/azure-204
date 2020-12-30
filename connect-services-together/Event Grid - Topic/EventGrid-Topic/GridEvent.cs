using System;
using System.Collections.Generic;
using System.Text;

namespace EventGrid_Topic
{
    public class GridEvent
    {
        public string Id { get; set; }
        public string Subject { get; set; }
        public string Topic { get; set; }
        public string EventType { get; set; }
        public customer Data { get; set; }
        public DateTime EventTime { get; set; }
    }
}
