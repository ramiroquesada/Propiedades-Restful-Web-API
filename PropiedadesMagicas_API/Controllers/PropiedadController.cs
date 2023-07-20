using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropiedadesMagicas_API.Datos;
using PropiedadesMagicas_API.Models;
using PropiedadesMagicas_API.Models.Dto;

namespace PropiedadesMagicas_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropiedadController : ControllerBase
    {
        private readonly ILogger<PropiedadController> _logger;

        private readonly ApplicationDbContext _db;

        public PropiedadController(ILogger<PropiedadController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<PropiedadDto>> GetPropiedades()
        {
            _logger.LogInformation("Obtener todas las propiedades");
            return Ok(_db.Propiedades.ToList());
        }

        [HttpGet("id:int", Name = "GetPropiedad")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PropiedadDto> GetPropiedad(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Error al obtener la Propiedad con id " + id);
                return BadRequest();
            }

            //var propiedad = PropiedadStore.propiedadList.FirstOrDefault(p => p.Id == id);

            var propiedad = _db.Propiedades.FirstOrDefault(p => p.Id == id);

            if (propiedad == null)

            {
                return NotFound();
            }

            return Ok(propiedad);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PropiedadDto> NewPropiedad([FromBody] PropiedadDto propiedadDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_db.Propiedades.FirstOrDefault(p => p.Nombre.ToLower() == propiedadDto.Nombre.ToLower()) != null)
            {
                ModelState.AddModelError("NombreExiste", "La propiedad con ese nombre ya existe!");
                return BadRequest(ModelState);
            }

            if (propiedadDto == null)
            {
                return BadRequest();
            }

            if (propiedadDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            Propiedad modelo = new()
            {
                Nombre = propiedadDto.Nombre,
                Detalles = propiedadDto.Detalles,
                ImagenUrl = propiedadDto.ImagenUrl,
                Ocupantes = propiedadDto.Ocupantes,
                Tarifa = propiedadDto.Tarifa,
                MetrosCuadrados = propiedadDto.MetrosCuadrados,
                Amenidad = propiedadDto.Amenidad
            };

            _db.Propiedades.Add(modelo);
            _db.SaveChanges();

            return CreatedAtRoute("GetPropiedad", new { id = propiedadDto.Id }, propiedadDto);
        }

        [HttpDelete("id: int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeletePropiedad(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var propiedad = _db.Propiedades.FirstOrDefault(p => p.Id == id);

            if (propiedad == null)
            {
                return NotFound();
            }

            _db.Propiedades.Remove(propiedad);
            _db.SaveChanges();

            return NoContent();
        }

        [HttpPut("id: int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePropiedad(int id, [FromBody] PropiedadDto propiedadDto)
        {
            if (propiedadDto == null || id != propiedadDto.Id)
            {
                return BadRequest();
            }

            //var propiedad = PropiedadStore.propiedadList.FirstOrDefault(p => p.Id == id);

            //propiedad.Nombre = propiedadDto.Nombre;
            //propiedad.Ocupantes = propiedadDto.Ocupantes;
            //propiedad.MetrosCuadrados = propiedadDto.MetrosCuadrados;

            Propiedad modelo = new()
            {
                Id = propiedadDto.Id,
                Nombre = propiedadDto.Nombre,
                Detalles = propiedadDto.Detalles,
                ImagenUrl = propiedadDto.ImagenUrl,
                Ocupantes = propiedadDto.Ocupantes,
                Tarifa = propiedadDto.Tarifa,
                MetrosCuadrados = propiedadDto.MetrosCuadrados,
                Amenidad = propiedadDto.Amenidad
            };

            _db.Propiedades.Update(modelo);
            _db.SaveChanges();

            return NoContent();
        }

        [HttpPatch("id: int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialPropiedad(int id, JsonPatchDocument<PropiedadDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }

            var propiedad = _db.Propiedades.AsNoTracking().FirstOrDefault(p => p.Id == id);

            PropiedadDto propiedadDto = new()
            {
                Id = propiedad.Id,
                Nombre = propiedad.Nombre,
                Detalles = propiedad.Detalles,
                ImagenUrl = propiedad.ImagenUrl,
                Ocupantes = propiedad.Ocupantes,
                Tarifa = propiedad.Tarifa,
                MetrosCuadrados = propiedad.MetrosCuadrados,
                Amenidad = propiedad.Amenidad
            };

            if (propiedad == null) return BadRequest();

            patchDto.ApplyTo(propiedadDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Propiedad modelo = new()
            {
                Id = propiedadDto.Id,
                Nombre = propiedadDto.Nombre,
                Detalles = propiedadDto.Detalles,
                ImagenUrl = propiedadDto.ImagenUrl,
                Ocupantes = propiedadDto.Ocupantes,
                Tarifa = propiedadDto.Tarifa,
                MetrosCuadrados = propiedadDto.MetrosCuadrados,
                Amenidad = propiedadDto.Amenidad
            };

            _db.Propiedades.Update(modelo);
            _db.SaveChanges();

            return NoContent();
        }
    }
}