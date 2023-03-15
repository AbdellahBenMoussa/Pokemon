using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReview.Core.Interfaces;
using PokemonReview.Core.Models.Dtos;
using PokemonReview.Core.Models.Entities;

namespace PokemonReview.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CountryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); ;
        }

        [HttpGet("GetAllCountries")]
        public async Task<ActionResult> GetAllCountriesAsync() =>
           !ModelState.IsValid ?
                BadRequest(ModelState) :
                Ok(_mapper.Map<List<CountryDto>>(await _unitOfWork.countries.GetAllTiesAsync()));

        [HttpGet("GetCountryById")]
        public async Task<IActionResult> GetCountryByIdAsync(int id) =>
         await _unitOfWork.countries.IsTExistAsync(c => c.Id == id) ?
            !ModelState.IsValid ?
                 BadRequest(ModelState) :
                     Ok(_mapper.Map<CountryDto>(
                         await _unitOfWork.countries.GetTByIdAsync(id))) :
                            NotFound($"No Country has an id = {id} !");

        [HttpGet("GetCountryByName")]
        public async Task<ActionResult> GetCountryByNameAsync(string name) =>
                await _unitOfWork.countries.IsTExistAsync(c => c.Name == name) ?
                  !ModelState.IsValid ?
                     BadRequest(ModelState) :
                         Ok(_mapper.Map<CountryDto>(
                             await _unitOfWork.countries.FindTByFilterAsync(c => c.Name == name))) :
                         NotFound($"No Country has {name} as name !");

        [HttpGet("GetAllCountriesContainsName")]
        public async Task<ActionResult> GetAllCountriesContainsNameAsync(string name) =>
           !ModelState.IsValid ?
                     BadRequest(ModelState) :
                         Ok(_mapper.Map<List<CountryDto>>(
                             await _unitOfWork.countries.FindAllTiesByFilterAsync(c => c.Name.Contains(name))));

        [HttpGet("GetAllCountriesContainsNameOrdered")]
        public async Task<ActionResult> GetAllCountriesContainsNameOrderedAsync(string name) => !ModelState.IsValid ?
                     BadRequest(ModelState) :
            Ok(_mapper.Map<List<CountryDto>>(await _unitOfWork.countries.FindAllTiesByFilterAsync(c => c.Name.Contains(name),
                null, null, c => c.Name, "DESC")));

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddCountryAsync([FromForm] CountryDto countryDto)
        {
            if (countryDto == null)
                return BadRequest(ModelState);

            if (await _unitOfWork.countries.IsTExistAsync(c => c.Name == countryDto.Name))
            {
                ModelState.AddModelError("", "This Country already exists!");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var country = await _unitOfWork.countries.AddTAsync(_mapper.Map<Country>(countryDto));

            if (_unitOfWork.Complete() <= 0)
            {
                ModelState.AddModelError("", "Error while saving!");
                return StatusCode(500, ModelState);
            }
            //return Ok("Country successfully saved.");
            return StatusCode(201, _mapper.Map<CountryDto>(country));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateCountry(int id, [FromForm] CountryUpdateDto countryUpdateDto)
        {
            if (countryUpdateDto == null)
                return BadRequest(ModelState);

            if (id != countryUpdateDto.Id)
                return BadRequest(ModelState);

            if (!await _unitOfWork.countries.IsTExistAsync(c => c.Id == id))
                return NotFound("Country not found !");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var country = _mapper.Map<Country>(countryUpdateDto);

            _unitOfWork.countries.UpdateT(country);

            if (_unitOfWork.Complete() <= 0)
            {
                ModelState.AddModelError("", "Error while updating!");
                return StatusCode(500, ModelState);
            }

            //return NoContent();
            return Ok(_mapper.Map<CountryDto>(country));
        }

        [HttpDelete("{countryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteCountry(int countryId)
        {
            if (!await _unitOfWork.countries.IsTExistAsync(c => c.Id == countryId))
            {
                return NotFound("Country not found");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _unitOfWork.countries.DeleteT(await _unitOfWork.countries.GetTByIdAsync(countryId));

            if (_unitOfWork.Complete() <= 0)
            {
                ModelState.AddModelError("", "Error while updating!");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
