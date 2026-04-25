using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CategoryController : Controller
  {
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
    {
      _categoryRepository = categoryRepository;
      _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetCategories()
    {
      var categories = _categoryRepository.GetCategories();
      var mappedCategories = _mapper.Map<List<CategoryDto>>(categories);
      return Ok(mappedCategories);
    }

    [HttpGet("{id}")]
    public IActionResult GetCategory(int id)
    {
      if (!_categoryRepository.CategoryExists(id))
      {
        return NotFound();
      }
      var category = _categoryRepository.GetCategory(id);
      var mappedCategory = _mapper.Map<CategoryDto>(category);
      return Ok(mappedCategory);
    }

    [HttpGet("GetPokemonByCategory/{categoryId}")]
    public IActionResult GetPokemonsByCategory(int categoryId)
    {
      var pokemon = _categoryRepository.GetPokemonsByCategory(categoryId);
      var mappedPokemons = _mapper.Map<List<PokemonDto>>(pokemon);
      return Ok(mappedPokemons);
    }

    [HttpPost]
    public IActionResult CreateCategory([FromBody] CategoryDto categoryPayload)
    {
      if (categoryPayload == null)
      {
        return BadRequest(ModelState);
      }
      
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if(_categoryRepository.CategoryExistsByName(categoryPayload.Name))
      {
        ModelState.AddModelError("", "Category Already Exists");
        return StatusCode(422, ModelState);
      }

      var categoryMap = _mapper.Map<Category>(categoryPayload);

      if (!_categoryRepository.CreateCategory(categoryMap))
      {
        ModelState.AddModelError("", "Something went wrong while Saving.");
        return StatusCode(500, ModelState);
      }

      return Ok("Successfully created.");
    }

    [HttpPut]
    public IActionResult UpdateCategory([FromBody] CategoryDto categoryPayload)
    {
      if (categoryPayload == null)
      {
        return BadRequest(ModelState);
      }
      
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if(!_categoryRepository.CategoryExists(categoryPayload.Id))
      {
        return NotFound();
      }

      if(_categoryRepository.CategoryExistsByName(categoryPayload.Name))
      {
        ModelState.AddModelError("", "Category Already Exists");
        return StatusCode(422, ModelState);
      }

      var categoryMap = _mapper.Map<Category>(categoryPayload);

      if (!_categoryRepository.UpdateCategory(categoryMap))
      {
        ModelState.AddModelError("", "Something went wrong while Saving.");
        return StatusCode(500, ModelState);
      }

      return Ok("Successfully updated.");
    }
  }
}