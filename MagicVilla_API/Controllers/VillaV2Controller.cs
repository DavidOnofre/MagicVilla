using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_API.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class VillaV2Controller : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        private readonly ApplicationDbContext _db;

        public VillaV2Controller(ILogger<VillaController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult<IEnumerable<VillaDto>> GetTowns()
        {
            _logger.LogInformation("Consultando todas las villas");
            return Ok(_db.Villas.ToList());
        }

        [HttpGet("{id:int}", Name = "GetVillaV2")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDto> GetTown(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Error con la villa con id " + id);
                return BadRequest();
            }

            var villa = _db.Villas.FirstOrDefault(villa => villa.Id == id);
            if (villa == null)
            {
                return NotFound();
            }

            return Ok(villa);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<VillaDto> SaveTown([FromBody] VillaDto villaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_db.Villas.FirstOrDefault(v => v.Name.ToLower() == villaDto.Name.ToLower()) != null)
            {
                ModelState.AddModelError("NombreExiste", "La villa con ese nombre ya existe");
                return BadRequest(ModelState);
            }

            if (villaDto == null)
            {
                return BadRequest();
            }
              
            Villa models = new()
            {
                Name = villaDto.Name,
                Detail = villaDto.Detail,
                ImageUrl = villaDto.ImageUrl,
                Occupants = villaDto.Occupants,
                Rate = villaDto.Rate,
                Area = villaDto.Area,
                Amenidad = villaDto.Amenidad,
                CreatedDate = DateTime.Now,
                UpdateDate = DateTime.Now,
            };

            _db.Villas.Add(models);
            _db.SaveChanges(); //para que los cambios se reflejen en la base de datos.

            return CreatedAtRoute("GetVillaV2", new { id = villaDto.Id }, villaDto);

        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteTown(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var villa = _db.Villas.FirstOrDefault(v => v.Id == id);
            if (villa == null)
            {
                return NotFound();
            }

            _db.Villas.Remove(villa);
            _db.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDto villaDto)
        {
            if (villaDto == null || id != villaDto.Id)
            {
                return BadRequest();
            }

            Villa model = new()
            {
                Id = villaDto.Id,
                Name = villaDto.Name,
                Detail = villaDto.Detail,
                ImageUrl = villaDto.ImageUrl,
                Occupants = villaDto.Occupants,
                Rate = villaDto.Rate,
                Area = villaDto.Area,
                Amenidad = villaDto.Amenidad,
                UpdateDate = DateTime.Now
            };

            _db.Villas.Update(model);
            _db.SaveChanges();
            return NoContent();
        }
    }

}
