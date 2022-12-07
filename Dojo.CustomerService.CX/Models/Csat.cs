using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace Dojo.CustomerService.CX.Models
{
    public class Csat
    {
        [BsonId()]
        // [JsonIgnore]
        public Guid Id { get; set; }
        public int Score { get; set; }
        public string Comment { get; set; }
        public bool ProblemSolved { get; set; }
        public string AttendantEmail { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
