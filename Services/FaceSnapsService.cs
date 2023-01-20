using SnapFaceApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace SnapFaceApi.Services;

public class FaceSnapsService
{
  private readonly IMongoCollection<FaceSnapItem> _facesnapsCollection;

  public FaceSnapsService(
      IOptions<FacesnapStoreDatabaseSettings> facesnapStoreDatabaseSettings)
  {
    var mongoClient = new MongoClient(
        facesnapStoreDatabaseSettings.Value.ConnectionString);

    var mongoDatabase = mongoClient.GetDatabase(
        facesnapStoreDatabaseSettings.Value.DatabaseName);

    _facesnapsCollection = mongoDatabase.GetCollection<FaceSnapItem>(
        facesnapStoreDatabaseSettings.Value.FacesnapsCollectionName);
  }

  public async Task<List<FaceSnapItem>> GetAsync() =>
      await _facesnapsCollection.Find(_ => true).ToListAsync();

  public async Task<FaceSnapItem?> GetAsync(string id) =>
      await _facesnapsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

  public async Task CreateAsync(FaceSnapItem newFacesnap) =>
      await _facesnapsCollection.InsertOneAsync(newFacesnap);

  public async Task UpdateAsync(string id, FaceSnapItem updatedFacesnap) =>
      await _facesnapsCollection.ReplaceOneAsync(x => x.Id == id, updatedFacesnap);

  public async Task RemoveAsync(string id) =>
      await _facesnapsCollection.DeleteOneAsync(x => x.Id == id);
}