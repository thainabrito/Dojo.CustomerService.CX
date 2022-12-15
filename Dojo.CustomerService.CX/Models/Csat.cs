using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace Dojo.CustomerService.CX.Models
{
    public class Csat
    {
        [BsonId()]
        public Guid Id { get; set; }
        public int Score { get; set; }
        public string Comment { get; set; }
        public bool ProblemSolved { get; set; }
        public string AttendantEmail { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
