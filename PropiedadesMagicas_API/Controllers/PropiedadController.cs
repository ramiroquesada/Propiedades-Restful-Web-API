﻿using AutoMapper;
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

        private readonly IMapper _mappper;

        public PropiedadController(ILogger<PropiedadController> logger, ApplicationDbContext db, IMapper mapper)
        {
            _logger = logger;
            _db = db;
            _mappper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PropiedadDto>>> GetPropiedades()
        {
            _logger.LogInformation("Obtener todas las propiedades");

            IEnumerable<Propiedad> propiedadList = await _db.Propiedades.ToListAsync();

            return Ok(_mappper.Map<IEnumerable<PropiedadDto>>(propiedadList));
        }

        [HttpGet("id:int", Name = "GetPropiedad")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PropiedadDto>> GetPropiedad(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Error al obtener la Propiedad con id " + id);
                return BadRequest();
            }

            //var propiedad = PropiedadStore.propiedadList.FirstOrDefault(p => p.Id == id);

            var propiedad = await _db.Propiedades.FirstOrDefaultAsync(p => p.Id == id);

            if (propiedad == null)

            {
                return NotFound();
            }

            return Ok(_mappper.Map<PropiedadDto>(propiedad));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PropiedadDto>> NewPropiedad([FromBody] PropiedadCreateDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _db.Propiedades.FirstOrDefaultAsync(p => p.Nombre.ToLower() == createDto.Nombre.ToLower()) != null)
            {
                ModelState.AddModelError("NombreExiste", "La propiedad con ese nombre ya existe!");
                return BadRequest(ModelState);
            }

            if (createDto == null)
            {
                return BadRequest(createDto);
            }

            Propiedad modelo = _mappper.Map<Propiedad>(createDto);

            await _db.Propiedades.AddAsync(modelo);
            await _db.SaveChangesAsync();

            return CreatedAtRoute("GetPropiedad", new { id = modelo.Id }, modelo);
        }

        [HttpDelete("id: int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePropiedad(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var propiedad = await _db.Propiedades.FirstOrDefaultAsync(p => p.Id == id);

            if (propiedad == null)
            {
                return NotFound();
            }

            _db.Propiedades.Remove(propiedad);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("id: int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePropiedad(int id, [FromBody] PropiedadUpdateDto updateDto)
        {
            if (updateDto == null || id != updateDto.Id)
            {
                return BadRequest();
            }

            Propiedad modelo = _mappper.Map<Propiedad>(updateDto);

            _db.Propiedades.Update(modelo);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("id: int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialPropiedad(int id, JsonPatchDocument<PropiedadUpdateDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }

            var propiedad = await _db.Propiedades.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

            PropiedadUpdateDto propiedadDto = _mappper.Map<PropiedadUpdateDto>(propiedad);

            if (propiedad == null) return BadRequest();

            patchDto.ApplyTo(propiedadDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Propiedad modelo = _mappper.Map<Propiedad>(propiedadDto);

            _db.Propiedades.Update(modelo);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}