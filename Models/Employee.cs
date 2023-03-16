using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using WebApplication3.Services;

namespace WebApplication3.Models;

public class Employee
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? empID { get; set; }
    [BsonElement("firstName")]
    public string firstName { get; set; }
    [BsonElement("lastName")]
    public string lastName { get; set; }
    [BsonElement("empRole")]
    public string empRole { get; set; }
}