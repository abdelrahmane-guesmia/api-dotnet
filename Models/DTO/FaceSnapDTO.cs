using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace SnapFaceApi.Models;
public class FaceSnapDTO
{
  [BsonElement("title")]
  [JsonPropertyName("title")]
  public string? Title { get; set; }
  [BsonElement("description")]
  [JsonPropertyName("description")]
  public string? Description { get; set; }
  [BsonElement("created_date")]
  [JsonPropertyName("createdDate")]
  public DateTime CreatedDate { get; set; }
  [BsonElement("snaps")]
  [JsonPropertyName("snaps")]
  public int Snaps { get; set; }
  [BsonElement("image_url")]
  [JsonPropertyName("imageUrl")]
  public string? ImageUrl { get; set; }
  [BsonElement("location")]
  [JsonPropertyName("location")]
  public string? Location { get; set; }
}