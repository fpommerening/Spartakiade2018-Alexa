using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Service.Data
{
    public class Appointment
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("externalid")]
        public Guid ExternalId { get; set; }

        [BsonElement("date")]
        public DateTime Date { get; set; }

        [BsonElement("type")]
        public AppointmentType Type { get; set; }

        [BsonElement("createon")]
        public DateTime CreateOn { get; set; }

        [BsonElement("userid")]
        public string UserId { get; set; }

        public List<Participant> Participants { get; set; }
    }
}
