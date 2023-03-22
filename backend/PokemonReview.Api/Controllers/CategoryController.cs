using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReview.Core.Interfaces;
using PokemonReview.Core.Models.Dtos;
using PokemonReview.Core.Models.Entities;

namespace PokemonReview.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); ;
        }

        [HttpGet("GetAllCategories")]
        public async Task<ActionResult> GetAllCategoriesAsync() =>
            !ModelState.IsValid ?
                 BadRequest(ModelState) :
                 Ok(_mapper.Map<List<CategoryUpdateDto>>(await _unitOfWork.categories.GetAllTiesAsync()));

        [HttpGet("GetCategoryById")]
        public async Task<IActionResult> GetCategoryByIdAsync(int id) =>
         await _unitOfWork.categories.IsTExistAsync(c => c.Id == id) ?
            !ModelState.IsValid ?
                 BadRequest(ModelState) :
                     Ok(_mapper.Map<CategoryDto>(
                         await _unitOfWork.categories.GetTByIdAsync(id))) :
                            NotFound($"No category has an id = {id} !");

        [HttpGet("GetCategoryByName")]
        public async Task<ActionResult> GetCategoryByNameAsync(string name) =>
                await _unitOfWork.categories.IsTExistAsync(c => c.Name == name) ?
                  !ModelState.IsValid ?
                     BadRequest(ModelState) :
                         Ok(_mapper.Map<CategoryDto>(
                             await _unitOfWork.categories.FindTByFilterAsync(c => c.Name == name))) :
                         NotFound($"No category has {name} as name !");

        [HttpGet("GetAllCategoriesContainsName")]
        public async Task<ActionResult> GetAllCategoriesContainsNameAsync(string name) =>
           !ModelState.IsValid ?
                     BadRequest(ModelState) :
                         Ok(_mapper.Map<List<CategoryDto>>(
                             await _unitOfWork.categories.FindAllTiesByFilterAsync(c => c.Name.Contains(name))));

        [HttpGet("GetAllCategoriesContainsNameOrdered")]
        public async Task<ActionResult> GetAllCategoriesContainsNameOrderedAsync(string name) => !ModelState.IsValid ?
                     BadRequest(ModelState) :
            Ok(_mapper.Map<List<CategoryDto>>(await _unitOfWork.categories.FindAllTiesByFilterAsync(c => c.Name.Contains(name),
                null, null, c => c.Name, "DESC")));

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddCategoryAsync([FromForm] CategoryDto categoryDto)
        {
            if (categoryDto == null)
                return BadRequest(ModelState);

            if (await _unitOfWork.categories.FindTByFilterAsync(c => c.Name == categoryDto.Name) != null)
            {
                ModelState.AddModelError("", "This category already exists!");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = await _unitOfWork.categories.AddTAsync(_mapper.Map<Category>(categoryDto));

            if (_unitOfWork.Complete() <= 0)
            {
                ModelState.AddModelError("", "Error while saving!");
                return StatusCode(500, ModelState);
            }
            //return Ok("Category successfully saved.");
            return StatusCode(201, _mapper.Map<CategoryDto>(category));
        }
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryUpdateDto categoryUpdateDto)
        {
            if (categoryUpdateDto == null)
                return BadRequest(ModelState);

            if (id != categoryUpdateDto.Id)
                return BadRequest(ModelState);

            if (!await _unitOfWork.categories.IsTExistAsync(c => c.Id == id))
                return NotFound("Category not found !");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = _mapper.Map<Category>(categoryUpdateDto);

            _unitOfWork.categories.UpdateT(category);

            if (_unitOfWork.Complete() <= 0)
            {
                ModelState.AddModelError("", "Error while updating!");
                return StatusCode(500, ModelState);
            }

            //return NoContent();
            return Ok(_mapper.Map<CategoryDto>(category));
        }
        [HttpDelete("{categoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            if (!await _unitOfWork.categories.IsTExistAsync(c => c.Id == categoryId))
            {
                return NotFound("Category not found");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _unitOfWork.categories.DeleteT(await _unitOfWork.categories.GetTByIdAsync(categoryId));

            if (_unitOfWork.Complete() <= 0)
            {
                ModelState.AddModelError("", "Error while updating!");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
