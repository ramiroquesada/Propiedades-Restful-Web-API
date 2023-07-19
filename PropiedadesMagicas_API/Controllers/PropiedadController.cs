using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PropiedadesMagicas_API.Datos;
using PropiedadesMagicas_API.Models.Dto;

namespace PropiedadesMagicas_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropiedadController : ControllerBase
    {
        private readonly ILogger<PropiedadController> _logger;

        public PropiedadController(ILogger<PropiedadController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<PropiedadDto>> GetPropiedades()
        {
            _logger.LogInformation("Obtener todas las propiedades");
            return Ok(PropiedadStore.propiedadList);
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

            var propiedad = PropiedadStore.propiedadList.FirstOrDefault(p => p.Id == id);

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

            if (PropiedadStore.propiedadList.FirstOrDefault(p => p.Nombre.ToLower() == propiedadDto.Nombre.ToLower()) != null)
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

            propiedadDto.Id = PropiedadStore.propiedadList.OrderByDescending(v => v.Id).FirstOrDefault().Id + 1;

            PropiedadStore.propiedadList.Add(propiedadDto);

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

            var propiedad = PropiedadStore.propiedadList.FirstOrDefault(p => p.Id == id);

            if (propiedad == null)
            {
                return NotFound();
            }

            PropiedadStore.propiedadList.Remove(propiedad);

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

            var propiedad = PropiedadStore.propiedadList.FirstOrDefault(p => p.Id == id);

            propiedad.Nombre = propiedadDto.Nombre;
            propiedad.Ocupantes = propiedadDto.Ocupantes;
            propiedad.MetrosCuadrados = propiedadDto.MetrosCuadrados;

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

            var propiedad = PropiedadStore.propiedadList.FirstOrDefault(p => p.Id == id);

            patchDto.ApplyTo(propiedad, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();
        }
    }
}