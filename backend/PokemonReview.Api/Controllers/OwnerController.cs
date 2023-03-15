using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReview.Core.Interfaces;
using PokemonReview.Core.Models.Dtos;
using PokemonReview.Core.Models.Entities;

namespace PokemonReview.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OwnerController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); ;
        }

        [HttpGet("GetAllOwners")]
        public async Task<ActionResult> GetAllOwnersAsync() =>
           !ModelState.IsValid ?
                BadRequest(ModelState) :
                Ok(_mapper.Map<List<OwnerDto>>(await _unitOfWork.owners.GetAllTiesAsync()));

        [HttpGet("GetOwnerById")]
        public async Task<IActionResult> GetOwnerByIdAsync(int id) =>
         await _unitOfWork.owners.IsTExistAsync(c => c.Id == id) ?
            !ModelState.IsValid ?
                 BadRequest(ModelState) :
                     Ok(_mapper.Map<OwnerDto>(
                         await _unitOfWork.owners.GetTByIdAsync(id))) :
                            NotFound($"No Owner has an id = {id} !");

        [HttpGet("GetOwnerByLastName")]
        public async Task<ActionResult> GetOwnerByLastNameAsync(string name) =>
                await _unitOfWork.owners.IsTExistAsync(o => o.LastName == name) ?
                  !ModelState.IsValid ?
                     BadRequest(ModelState) :
                         Ok(_mapper.Map<OwnerGetDto>(
                             await _unitOfWork.owners.FindTByFilterAsync(o => o.LastName == name, new[] { "Country" }))) :
                         NotFound($"No Owner has {name} as name !");

        [HttpGet("GetAllOwnersContainsLastName")]
        public async Task<ActionResult> GetAllOwnersContainsLastNameAsync(string name) =>
           !ModelState.IsValid ?
                     BadRequest(ModelState) :
                         Ok(_mapper.Map<List<OwnerDto>>(
                             await _unitOfWork.owners.FindAllTiesByFilterAsync(o => o.LastName.Contains(name))));

        [HttpGet("GetAllOwnersContainsLastNameOrdered")]
        public async Task<ActionResult> GetAllOwnersContainsLastNameOrderedAsync(string name) => !ModelState.IsValid ?
                     BadRequest(ModelState) :
            Ok(_mapper.Map<List<OwnerDto>>(await _unitOfWork.owners.FindAllTiesByFilterAsync(c => c.LastName.Contains(name),
                null, null, c => c.LastName, "DESC")));

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddOwnerAsync(int countryId, [FromForm] OwnerDto ownerDto)
        {
            if (ownerDto == null)
                return BadRequest(ModelState);

            if (await _unitOfWork.owners.IsTExistAsync(c => c.LastName.Trim().ToLower() == ownerDto.LastName.Trim().ToLower() && c.FirstName.Trim().ToLower() == ownerDto.FirstName.Trim().ToLower()))
            {
                ModelState.AddModelError("", "This Owner already exists!");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var owner = _mapper.Map<Owner>(ownerDto);
            owner.Country = await _unitOfWork.countries.GetTByIdAsync(countryId);

            owner = await _unitOfWork.owners.AddTAsync(owner);

            if (_unitOfWork.Complete() <= 0)
            {
                ModelState.AddModelError("", "Error while saving!");
                return StatusCode(500, ModelState);
            }
            //return Ok("Owner successfully saved.");
            return StatusCode(201, _mapper.Map<OwnerDto>(owner));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateOwner(int id, [FromForm] OwnerUpdateDto countryUpdateDto)
        {
            if (countryUpdateDto == null)
                return BadRequest(ModelState);

            if (id != countryUpdateDto.Id)
                return BadRequest(ModelState);

            if (!await _unitOfWork.owners.IsTExistAsync(o => o.Id == id))
                return NotFound("Owner not found !");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var country = _mapper.Map<Owner>(countryUpdateDto);

            _unitOfWork.owners.UpdateT(country);

            if (_unitOfWork.Complete() <= 0)
            {
                ModelState.AddModelError("", "Error while updating!");
                return StatusCode(500, ModelState);
            }

            //return NoContent();
            return Ok(_mapper.Map<OwnerDto>(country));
        }

        [HttpDelete("{countryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteOwner(int countryId)
        {
            if (!await _unitOfWork.owners.IsTExistAsync(o => o.Id == countryId))
            {
                return NotFound("Owner not found");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _unitOfWork.owners.DeleteT(await _unitOfWork.owners.GetTByIdAsync(countryId));

            if (_unitOfWork.Complete() <= 0)
            {
                ModelState.AddModelError("", "Error while updating!");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
