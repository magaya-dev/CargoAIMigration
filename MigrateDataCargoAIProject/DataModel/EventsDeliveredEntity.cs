using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrateDataCargoAIProject.DataModel
{
    public class EventsDeliveredEntity : IEntity
    {
        [JsonProperty("_etag")]
        public string ETag { get; set; }

        [JsonProperty("id")]
        public string EventId { get; set; }

        public string PartitionKey => NetworkId;

        public string NetworkId { get; set; }
        public TrackingBookingInfo Event { get; set; }
        public bool FromNonDelivered { get; set; }
        public string CopyErrorDescription { get; set; }
        public DateTime CreatedDate { get; set; }
        public TrackingBookingInfo Origin { get; set; }
    }
}
