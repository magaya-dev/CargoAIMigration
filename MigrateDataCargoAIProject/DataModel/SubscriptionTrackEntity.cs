using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrateDataCargoAIProject.DataModel
{
    public class SubscriptionTrackEntity : IEntity
    {
        [JsonProperty("_etag")]
        public string ETag { get; set; }

        [JsonProperty("id")]
        public string SubscriptionId { get; set; }

        public string PartitionKey => NetworkId;

        public string NetworkId { get; set; }

        public string Awb { get; set; }

        public string Id { get; set; }
        public string Number { get; set; }
        public string Type { get; set; } // "bk" | "sh"

        public DateTime DtCreated { get; set; }

        public bool SubscriptionSuccess { get; set; }
        public string SubscriptionFailedMessage { get; set; }

        public bool AutoTracking { get; set; }
    }
}
