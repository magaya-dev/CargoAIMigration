using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrateDataCargoAIProject.DataModel
{
    public class AirlinesEntity : IEntity
    {
        [JsonProperty("_etag")]
        public string ETag { get; set; }

        [JsonProperty("id")]
        public string Code { get; set; }

        public string PartitionKey => Code;
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
