using AutoMapper;
using MagicVilla_API.Models;
using MagicVilla_API.Models.Dto;
using MagicVilla_API.Repositorio;
using MagicVilla_API.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MagicVilla_API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class NumeroVillaController : ControllerBase
    {
        private readonly ILogger<NumeroVillaController> _logger;
        private readonly INumeroVillaRepositorio _numeroVillaRepositorio;
        private readonly IVillaRepositorio _villaRepositorio;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        //constructor
        public NumeroVillaController(ILogger<NumeroVillaController> logger, INumeroVillaRepositorio numeroVillaRepositorio, IVillaRepositorio villaRepositorio, IMapper mapper)
        {
            _logger = logger;
            _numeroVillaRepositorio = numeroVillaRepositorio;
            _villaRepositorio = villaRepositorio;
            _mapper = mapper;
            _response = new APIResponse(); 
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<APIResponse>> GetNumeroVillas()
        {
            try
            {
                _logger.LogInformation("Consultando todas numero villas");

                IEnumerable<NumeroVilla> villaList = await _numeroVillaRepositorio.GetAll();

                _response.Result = _mapper.Map<IEnumerable<NumeroVillaDto>>(villaList);
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

        [HttpGet("{id:int}", Name = "GetNumeroVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetNumeroVilla(int id)
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

                var numeroVilla = await _numeroVillaRepositorio.GetById(villa => villa.VillaNo == id);
                if (numeroVilla == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.isSuccess = false;
                    return NotFound(_response);
                }

                _response.Result = _mapper.Map<NumeroVillaDto>(numeroVilla);
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
        public async Task<ActionResult<APIResponse>> SaveTown([FromBody] NumeroVillaCreateDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _numeroVillaRepositorio.GetById(v => v.VillaNo == createDto.VillaNo) != null)
                {
                    ModelState.AddModelError("NumeroVilla", "El Numero ya existe");
                    return BadRequest(ModelState);
                }

                if (await _villaRepositorio.GetById(v=>v.Id == createDto.VillaId) == null) {
                    ModelState.AddModelError("ClaveForanea", "El id de la villa no existe");
                    return BadRequest(ModelState);
                }

                if (createDto == null)
                {
                    return BadRequest(createDto);
                }

                NumeroVilla model = _mapper.Map<NumeroVilla>(createDto);
                model.UpdatedDate = DateTime.Now;
                model.CreatedDate = DateTime.Now;
                await _numeroVillaRepositorio.Create(model);

                _response.Result = model;
                _response.StatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetNumeroVilla", new { id = model.VillaNo }, _response);
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

                var numeroVilla = await _numeroVillaRepositorio.GetById(v => v.VillaNo == id);
                if (numeroVilla == null)
                {
                    _response.isSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                await _numeroVillaRepositorio.Remove(numeroVilla);

                _response.StatusCode = HttpStatusCode.NoContent;

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
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] NumeroVillaUpdateDto updateDto)
        {
            if (updateDto == null || id != updateDto.VillaNo)
            {
                _response.isSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            if (await _villaRepositorio.GetById(v => v.Id == updateDto.VillaId) == null) {
                ModelState.AddModelError("ClaveForaneaUpdate","El id de la villa no existe");
            }

            NumeroVilla model = _mapper.Map<NumeroVilla>(updateDto);

            await _numeroVillaRepositorio.Update(model);
            _response.StatusCode = HttpStatusCode.NoContent;

            return Ok(_response);
        }

    }
}
