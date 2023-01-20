using Microsoft.EntityFrameworkCore;

namespace SnapFaceApi.Models
{
  public class FaceSnapContext : DbContext
  {
    public FaceSnapContext(DbContextOptions<FaceSnapContext> options) : base(options) { }
    public DbSet<FaceSnapItem> FaceSnapItems { get; set; } = null!;
  }
}