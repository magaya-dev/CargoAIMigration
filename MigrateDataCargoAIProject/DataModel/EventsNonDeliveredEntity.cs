using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrateDataCargoAIProject.DataModel
{
    public class EventsNonDeliveredEntity : IEntity
    {
        [JsonProperty("_etag")]
        public string ETag { get; set; }

        [JsonProperty("id")]
        public string EventId { get; set; }

        public string PartitionKey => NetworkId;

        public string NetworkId { get; set; }
        public TrackingBookingInfo Event { get; set; }
        public string ErrorDescription { get; set; }
        public DateTime CreatedDate { get; set; }

        public TrackingBookingInfo Origin { get; set; }
    }

    public class TrackingBookingInfo
    {
        public string FlightUUID { get; set; }
        [Required]
        public string Awb { get; set; }
        public string Weight { get; set; }
        public string Volume { get; set; }
        public string Pieces { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public Event[] Events { get; set; }
        public Event[] OldEvents { get; set; }
        public string OriginCoord { get; set; }
        public string DestinationCoord { get; set; }
        public string Status { get; set; }

        public string Id { get; set; }
        public string Number { get; set; }
        public string Type { get; set; }
    }

    public class Event
    {
        public string Code { get; set; }
        public DateTime? EventDate { get; set; }
        public string EventLocation { get; set; }
        public string EventLocationCoord { get; set; }
        public Flight Flight { get; set; }
        public string Weight { get; set; }
        public string Volume { get; set; }
        public string Pieces { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string FlightNumber { get; set; }
        public string CarbonEmission { get; set; }
    }

    public class Flight
    {
        public string Number { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string CarbonEmission { get; set; }
    }
}
