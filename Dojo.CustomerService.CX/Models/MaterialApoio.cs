using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Dojo.CustomerService.CX.Models
{
    public class MaterialApoio
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int Score { get; set; }
        public string Comment { get; set; }
        public bool ProblemSolved { get; set; }
        public string AttendantEmail { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
