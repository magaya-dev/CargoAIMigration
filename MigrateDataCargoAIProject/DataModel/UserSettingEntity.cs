using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrateDataCargoAIProject.DataModel
{
    public class UserSettingEntity : IEntity
    {
        public UserSettingEntity()
        {
            AutoTrackingOnDate = null;
        }

        [JsonProperty("_etag")]
        public string ETag { get; set; }

        [JsonProperty("id")]
        public string NetworkId { get; set; }

        public string PartitionKey => NetworkId;
        public UserApiData Credentials { get; set; }

        public SettingInfo Settings { get; set; }
        public IEnumerable<ChargeSetting> Charges { get; set; }

        public bool AutoTracking { get; set; }
        public DateTime? AutoTrackingOnDate { get; set; }

        public string PrefixBooking { get; set; }
        public int NumberBooking { get; set; }
        public int Digits { get; set; }
    }

    public class SettingInfo
    {
        public SettingInfo()
        {
            EmailsDestination = new List<string>();
        }
        public IEnumerable<string> EmailsDestination { get; set; }
    }

    public class UserApiData
    {
        public string ApiUsername { get; set; }
        public string ApiUserPassword { get; set; }
    }

    public enum ChargeType { income, expense }
    public class ChargeSetting
    {
        public ChargeType Type { get; set; }
        public string ChargeCode { get; set; }

    }
}
