using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrateDataCargoAIProject.DataModel
{
    public class AutoTrackingTransactionDataEntity : IEntity
    {
        public AutoTrackingTransactionDataEntity()
        {
            AllRetrieveFailed = false;
        }

        [JsonProperty("_etag")]
        public string ETag { get; set; }

        public string PartitionKey => NetworkId;

        public string NetworkId { get; set; }

        public string Awb { get; set; }

        [JsonProperty("id")]
        public string TransactionGuid { get; set; }
        public string TransactionNumber { get; set; }
        public string TrasactionType { get; set; } // "bk" | "sh"

        public DateTime CreateOn { get; set; }

        public bool AutoSubscriptionSuccess { get; set; }
        public string AutoSubscriptionFailedDetail { get; set; }

        public int? RetrieveCount { get; set; }
        public bool AllRetrieveFailed { get; set; }
    }
}
