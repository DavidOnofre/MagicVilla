using AutoMapper;
using MagicVilla_API.Models;
using MagicVilla_API.Models.Dto;
using MagicVilla_API.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MagicVilla_API.Controllers
{
    [Route("api/v7/[controller]")]
    [ApiController]
    public class VillaV7Controller : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        private readonly IVillaRepositorio _villaRepositorio;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        //constructor
        public VillaV7Controller(ILogger<VillaController> logger, IVillaRepositorio villaRepositorio, IMapper mapper)
        {
            _logger = logger;
            _villaRepositorio = villaRepositorio;
            _mapper = mapper;
            _response = new APIResponse(); // este parametro no esta inyectado, solo se lo inicia en el constructor
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<APIResponse>> GetTowns()
        {
            try
            {
                _logger.LogInformation("Consultando todas las villas");

                IEnumerable<Villa> villaList = await _villaRepositorio.GetAll();

                _response.Result = _mapper.Map<IEnumerable<VillaDto>>(villaList);
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error en el metodo getAll" + ex.Message);
                _response.isSuccess = false;
                _response.ErrorMessagge = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("{id:int}", Name = "GetVillaV7")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetTown(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error con la villa con id " + id);
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.isSuccess = false;
                    return BadRequest(_response);
                }

                var villa = await _villaRepositorio.GetById(villa => villa.Id == id);
                if (villa == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.isSuccess = false;
                    return NotFound(_response);
                }

                _response.Result = _mapper.Map<VillaDto>(villa);
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.ErrorMessagge = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> SaveTown([FromBody] VillaCreateDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _villaRepositorio.GetById(v => v.Name.ToLower() == createDto.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("NombreExiste", "La villa con ese nombre ya existe");
                    return BadRequest(ModelState);
                }

                if (createDto == null)
                {
                    return BadRequest(createDto);
                }

                Villa model = _mapper.Map<Villa>(createDto);
                model.UpdateDate = DateTime.Now;
                model.CreatedDate = DateTime.Now;
                await _villaRepositorio.Create(model);

                _response.Result = model;
                _response.StatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetVillaV7", new { id = model.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.ErrorMessagge = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTown(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.isSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var villa = await _villaRepositorio.GetById(v => v.Id == id);
                if (villa == null)
                {
                    _response.isSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                await _villaRepositorio.Remove(villa);

                _response.StatusCode = HttpStatusCode.NoContent;

                //return NoContent();// NoContent() no acepta ningun parametro por lo que se usa OK()
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.ErrorMessagge = new List<string>() { ex.ToString() };
            }
            return BadRequest(_response);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDto updateDto)
        {
            if (updateDto == null || id != updateDto.Id)
            {
                _response.isSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            Villa model = _mapper.Map<Villa>(updateDto);

            await _villaRepositorio.Update(model);
            _response.StatusCode = HttpStatusCode.NoContent;

            return Ok(_response);
        }

    }
}
