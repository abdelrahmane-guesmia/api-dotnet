using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SnapFaceApi.Models;
using SnapFaceApi.Services;

namespace SnapFaceApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class FaceSnapController : ControllerBase
  {
    private readonly FaceSnapContext _context;
    private readonly FaceSnapsService _facesnapsService;

    public FaceSnapController(FaceSnapContext context, FaceSnapsService faceSnapsService)
    {
      _context = context;
      _facesnapsService = faceSnapsService;
    }

    // GET: api/FaceSnap
    [HttpGet]
    public async Task<List<FaceSnapDTO>> GetFaceSnapItems()
    {
      var faceSnapItems = await _facesnapsService.GetAsync();
      return faceSnapItems.Select(x => ItemToDTO(x)).ToList();
    }



    // GET: api/FaceSnap/<id>
    [HttpGet("{id}")]
    public async Task<ActionResult<FaceSnapDTO>> GetFaceSnapItem(string id)
    {
      var faceSnapItem = await _facesnapsService.GetAsync(id);

      if (faceSnapItem == null)
      {
        return NotFound();
      }

      return ItemToDTO(faceSnapItem);
    }

    // PUT: api/FaceSnap/<id>
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutFaceSnapItem(string id, FaceSnapDTO faceSnapDTO)
    {

      var faceSnapItem = await _facesnapsService.GetAsync(id);
      if (faceSnapItem == null)
      {
        return NotFound();
      }

      faceSnapItem.Title = faceSnapDTO.Title;
      faceSnapItem.Description = faceSnapDTO.Description;
      faceSnapItem.Snaps = faceSnapDTO.Snaps;
      faceSnapItem.Location = faceSnapDTO.Location;
      faceSnapItem.ImageUrl = faceSnapDTO.ImageUrl;

      try
      {
        await _facesnapsService.UpdateAsync(id, faceSnapItem);
      }
      catch (DbUpdateConcurrencyException) when (!FaceSnapItemExists(id))
      {
        return NotFound();
      }

      return NoContent();
    }

    // POST: api/FaceSnap
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<FaceSnapDTO>> PostFaceSnapItem(FaceSnapDTO faceSnapDTO)
    {

      var faceSnapItem = DTOtoItem(faceSnapDTO);
      try
      {
        await _facesnapsService.CreateAsync(faceSnapItem);
      }
      catch (DbUpdateException)
      {
        if (FaceSnapItemExists(faceSnapItem.Id))
        {
          return Conflict();
        }
        else
        {
          throw;
        }
      }
      return CreatedAtAction(nameof(GetFaceSnapItem), new { id = faceSnapItem.Id }, faceSnapItem);
    }

    // DELETE: api/FaceSnap/<id>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFaceSnapItem(string id)
    {
      var faceSnapItem = await _facesnapsService.GetAsync(id);
      if (faceSnapItem == null)
      {
        return NotFound();
      }
      await _facesnapsService.RemoveAsync(id);
      return NoContent();
    }

    private bool FaceSnapItemExists(string id)
    {
      return _context.FaceSnapItems.Any(e => e.Id == id);
    }

    private static FaceSnapDTO ItemToDTO(FaceSnapItem faceSnapItem) =>

      new FaceSnapDTO
      {
        Title = faceSnapItem.Title,
        Description = faceSnapItem.Description,
        CreatedDate = faceSnapItem.CreatedDate,
        Snaps = faceSnapItem.Snaps,
        ImageUrl = faceSnapItem.ImageUrl,
        Location = faceSnapItem.Location
      };

    private static FaceSnapItem DTOtoItem(FaceSnapDTO faceSnapDTO) =>

      new FaceSnapItem
      {
        Title = faceSnapDTO.Title,
        Description = faceSnapDTO.Description,
        CreatedDate = faceSnapDTO.CreatedDate,
        Snaps = faceSnapDTO.Snaps,
        ImageUrl = faceSnapDTO.ImageUrl,
        Location = faceSnapDTO.Location
      };

  }
}

