using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrateDataCargoAIProject.DataModel
{
    public class ServiceActivationEntity : IEntity
    {
        [JsonProperty("_etag")]
        public string ETag { get; set; }

        [JsonProperty("id")]
        public string ActivationId { get; set; }

        public string PartitionKey => NetworkId;

        public string NetworkId { get; set; }
        public bool TrackingService { get; set; }
        public bool EBookingService { get; set; }

        public DateTime DtCreated { get; set; }
        public DateTime? DtModified { get; set; }
    }
}
