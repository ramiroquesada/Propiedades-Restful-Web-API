using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropiedadesMagicas_API.Datos;
using PropiedadesMagicas_API.Models;
using PropiedadesMagicas_API.Models.Dto;
using PropiedadesMagicas_API.Repositorio.IRepositorio;
using System.Net;

namespace PropiedadesMagicas_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumeroPropiedadController : ControllerBase
    {
        private readonly ILogger<NumeroPropiedadController> _logger;

        private readonly IPropiedadRepositorio _propiedadRepo;
        private readonly INumeroPropiedadRepositorio _numeroPropiedadRepo;

        private readonly IMapper _mappper;

        protected APIResponse _response;

        public NumeroPropiedadController(ILogger<NumeroPropiedadController> logger, IPropiedadRepositorio propiedadRepo, IMapper mapper, INumeroPropiedadRepositorio numeroPropiedadRepo)
        {
            _logger = logger;
            _propiedadRepo = propiedadRepo;
            _mappper = mapper;
            _response = new();
            _numeroPropiedadRepo = numeroPropiedadRepo;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetNumeroPropiedades()
        {
            try
            {
                _logger.LogInformation("Obtener numeros de las propiedades");

                IEnumerable<NumeroPropiedad> numeroPropiedadList = await _numeroPropiedadRepo.ObtenerTodos();

                _response.Resultado = _mappper.Map<IEnumerable<PropiedadDto>>(numeroPropiedadList);

                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }

        [HttpGet("id:int", Name = "GetNumeroPropiedad")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetNumeroPropiedad(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al obtener numero de Propiedad con el id " + id);

                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsExitoso = false;

                    return BadRequest(_response);
                }

                var numeroPropiedad = await _numeroPropiedadRepo.Obtener(p => p.PropiedadId == id);

                if (numeroPropiedad == null)

                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsExitoso = false;
                    return NotFound(_response);
                }

                _response.Resultado = _mappper.Map<NumeroPropiedadDto>(numeroPropiedad);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return Ok(_response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> NewNumeroPropiedad([FromBody] NumeroPropiedadCreateDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _numeroPropiedadRepo.Obtener(p => p.PropiedadNum == createDto.PropiedadNum) != null)
                {
                    ModelState.AddModelError("NombreExiste", "La propiedad con ese nombre ya existe!");
                    return BadRequest(ModelState);
                }

                if (await _propiedadRepo.Obtener(p => p.Id == createDto.PropiedadId) == null)
                {
                    ModelState.AddModelError("ClaveForanea", "El id de la propiedad no existe!");
                    return BadRequest(ModelState);
                }

                if (createDto == null)
                {
                    return BadRequest(createDto);
                }

                NumeroPropiedad modelo = _mappper.Map<NumeroPropiedad>(createDto);

                modelo.FechaCreacion = DateTime.Now;
                modelo.FechaActualizacion = DateTime.Now;

                await _numeroPropiedadRepo.Crear(modelo);

                _response.Resultado = modelo;
                _response.StatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetNumeroPropiedad", new { id = modelo.PropiedadNum }, _response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }

        [HttpDelete("id: int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteNumeroPropiedad(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var numeroPropiedad = await _numeroPropiedadRepo.Obtener(p => p.PropiedadNum == id);

                if (numeroPropiedad == null)
                {
                    _response.IsExitoso = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                await _numeroPropiedadRepo.Remover(numeroPropiedad);

                _response.StatusCode = HttpStatusCode.NoContent;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return BadRequest(_response);
        }

        [HttpPut("id: int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateNumeroPropiedad(int id, [FromBody] NumeroPropiedadUpdateDto updateDto)
        {
            try
            {
                if (updateDto == null || id != updateDto.PropiedadNum)
                {
                    _response.IsExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                if (await _propiedadRepo.Obtener(p => p.Id == updateDto.PropiedadId) == null)
                {
                    ModelState.AddModelError("Clave foranea", "El id de la propiedad no existe!");
                    return BadRequest(ModelState);
                }

                NumeroPropiedad modelo = _mappper.Map<NumeroPropiedad>(updateDto);

                await _numeroPropiedadRepo.Actualizar(modelo);

                _response.StatusCode = HttpStatusCode.NoContent;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return Ok(_response);
        }
    }
}