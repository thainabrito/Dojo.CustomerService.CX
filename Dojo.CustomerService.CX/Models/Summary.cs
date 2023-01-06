using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace Dojo.CustomerService.CX.Models
{
    public class Summary
    {
        public decimal Score { get; set; }
        public ProblemSolved Fcr { get; set; }
    }
}
