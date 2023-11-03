using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrateDataCargoAIProject.DataModel
{
    public class AutoTrackingClientsEntity : IEntity
    {
        public AutoTrackingClientsEntity()
        {
            SettingDateModified = null;
        }

        [JsonProperty("_etag")]
        public string ETag { get; set; }

        public string PartitionKey => NetworkId;

        [JsonProperty("id")]
        public string NetworkId { get; set; }

        public DateTime ShLastDateCreated { get; set; }

        public DateTime BkLastDateCreated { get; set; }
        public DateTime? SettingDateModified { get; set; }
    }
}
