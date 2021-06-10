using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;  

namespace HeroesApi.Models
{
    public class Hero
    {
        [BsonId]
      //  [BsonRepresentation(BsonType.ObjectId)]
        [JsonProperty("id")]
        public string Id { get; set; }


        [BsonElement("Name")]
        [JsonProperty("name")]
        public string name { get; set; }

        public string description { get; set; }

        public DateTime Birthday  { get; set; }

    }
}