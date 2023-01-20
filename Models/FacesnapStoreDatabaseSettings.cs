namespace SnapFaceApi.Models;

public class FacesnapStoreDatabaseSettings
{
  public string ConnectionString { get; set; } = null!;

  public string DatabaseName { get; set; } = null!;

  public string FacesnapsCollectionName { get; set; } = null!;
}