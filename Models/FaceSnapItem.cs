using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace SnapFaceApi.Models;
public class FaceSnapItem
{
  [BsonId]
  [BsonRepresentation(BsonType.ObjectId)]
  public string? Id { get; set; }
  [BsonElement("title")]
  public string? Title { get; set; }
  [BsonElement("description")]
  public string? Description { get; set; }
  [BsonElement("created_date")]
  public DateTime CreatedDate { get; set; }
  [BsonElement("snaps")]
  public int Snaps { get; set; }
  [BsonElement("image_url")]
  public string? ImageUrl { get; set; }
  [BsonElement("location")]
  public string? Location { get; set; }
  [BsonElement("secret")]
  public string? Secret { get; set; }
}

