using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Text.Json.Serialization;

namespace KTFSWebservice.Models
{
    public class PlayerScore
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public String? Id { get; set; }
        public int Score { get; set; }
        [BsonElement("Name")]
        [JsonPropertyName("Name")]
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        public PlayerScore(int score, string name)
        {
            Score = score;
            Name = name;
            DateCreated = DateTime.Now;
            DateUpdated = DateTime.Now;
        }
    }
}
